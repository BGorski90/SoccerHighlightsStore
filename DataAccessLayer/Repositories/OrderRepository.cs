using SoccerHighlightsStore.BusinessLayer.Entities;
using SoccerHighlightsStore.DataAccessLayer.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SoccerHighlightsStore.Common.Infrastructure;
using SoccerHighlightsStore.Common.Contracts;
using PagedList;

namespace SoccerHighlightsStore.DataAccessLayer.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private SoccerVideoDbContext db;

        public OrderRepository()
        {
            db = new SoccerVideoDbContext();
        }

        public IEnumerable<Order> Orders
        {
            get
            {
                return db.Orders.OrderByDescending(o => o.OrderTime);
            }
        }

        public IEnumerable<Order> GetOrders(string sortBy = "OrderTime", int limit = int.MaxValue)
        {
            var sortByProperty = typeof(Order).GetProperty(sortBy);
            return db.Orders.OrderByDescending(o => sortByProperty.GetValue(o)).Take(limit);
                                            
        }

        public IEnumerable<Order> GetOrdersAscending(string sortBy = "OrderTime", int limit = int.MaxValue)
        {
            var sortByProperty = typeof(Order).GetProperty(sortBy);
            return db.Orders.OrderBy(o => sortByProperty.GetValue(o)).Take(limit);
        }

        public Order Get(int id)
        {
            return db.Orders.FirstOrDefault(o => o.OrderID == id);
        }

        public void Add(Order order)
        {
            order.OrderTime = DateTime.Now;
            db.Orders.Add(order);
            db.SaveChanges();
        }

        public void CreateOrder(Cart cart, string userId)
        {
            var videos = db.Videos.Where(v => cart.Videos.Contains(v.VideoID));
            var value = videos.Sum(v => v.Price);
            Order order = new Order
            {
                UserID = userId,
                Videos = videos.ToList(),
                OrderValue = value,
            };
            Add(order);
            if (userId != Consts.anonymousUserID)
                AddOrderInfoToUser(order);
        }
        public void AddOrderInfoToUser(Order order)
        {
            User buyer = db.Users.Find(order.UserID);
            int orderPoints = (int)order.OrderValue + 1 + (order.Videos.Count - 1) * SpecialOffers.pointsForExtraVideo;
            buyer.TotalOrders++;
            buyer.TotalSpending += order.OrderValue;
            buyer.TotalPoints += orderPoints;
            buyer.Orders.Add(order);
            db.SaveChanges();
        }
    }
}