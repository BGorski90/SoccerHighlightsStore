using SoccerHighlightsStore.BusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SoccerHighlightsStore.Common.Infrastructure;
using SoccerHighlightsStore.Common.Contracts;
using PagedList;

namespace SoccerHighlightsStore.DataAccessLayer.Repositories
{
    public class MockOrderRepository : IOrderRepository
    {
        public static readonly List<Order> _orders = new List<Order>();
        private static int _nextID = 1;

        public IEnumerable<Order> Orders
        {
            get
            {
                return _orders;
            }
        }

        public IEnumerable<Order> GetOrders(string sortBy = "OrderTime", int limit = int.MaxValue)
        {
            var sortByProperty = typeof(Order).GetProperty(sortBy);
            return _orders.OrderByDescending(o => sortByProperty.GetValue(o)).Take(limit);
        }

        public IEnumerable<Order> GetOrdersAscending(string sortBy = "OrderTime", int limit = int.MaxValue)
        {
            var sortByProperty = typeof(Order).GetProperty(sortBy);
            return _orders.OrderBy(o => sortByProperty.GetValue(o)).Take(limit);
        }

        public Order Get(int id)
        {
            return _orders.Find(o => o.OrderID == id);
        }

        public void Add(Order order)
        {
            order.OrderID = _nextID++;
            order.OrderTime = DateTime.Now;
            _orders.Add(order);
        }

        public void CreateOrder(Cart cart, string userId)
        {
        }

        public void AddOrderInfoToUser(Order order)
        {
        }
    }
}