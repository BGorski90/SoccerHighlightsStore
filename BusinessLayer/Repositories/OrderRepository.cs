using Storefront.BusinessLayer.Entities;
using Storefront.BusinessLayer.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Infrastructure;
using Common.Contracts;

namespace Storefront.BusinessLayer.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private SoccerVideoDbContext db;

        public OrderRepository()
        {
            db = new SoccerVideoDbContext();
        }

        public IQueryable<Order> GetAll(string sortBy = "OrderTime", string sortOrder = "desc", int limit = int.MaxValue)
        {
            return db.Orders.OrderByDescending(o => o.OrderTime);
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
            User buyer = db.Users.FirstOrDefault(u => u.Id == order.UserID);
            int orderPoints = (int)order.OrderValue + 1 + (order.Videos.Count - 1) * SpecialOffers.pointsForExtraVideo;
            buyer.TotalOrders++;
            buyer.TotalSpending += order.OrderValue;
            buyer.TotalPoints += orderPoints;
            buyer.Orders.Add(order);
            db.SaveChanges();
        }
    }
}