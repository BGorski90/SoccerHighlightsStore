using Storefront.BusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Infrastructure;

namespace Storefront.BusinessLayer.Repositories
{
    public class MockOrderRepository: IOrderRepository
    {
        public static readonly List<Order> _orders = new List<Order>();
        private static int _nextID = 1;

        public IQueryable<Order> GetAll(string sortBy = "OrderTime", string sortOrder = "desc", int limit = int.MaxValue)
        {
            var sortByProperty = typeof(Order).GetProperty(sortBy);
            var result = sortOrder == "desc" ? _orders.OrderByDescending(o => sortByProperty.GetValue(o)).AsQueryable().Take(limit)
                                            : _orders.OrderBy(o => sortByProperty.GetValue(o)).AsQueryable().Take(limit);
            return result;
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