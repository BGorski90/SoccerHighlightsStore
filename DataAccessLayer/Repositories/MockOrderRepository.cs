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

        public IPagedList<Order> Orders
        {
            get
            {
                return _orders.ToPagedList(Consts.defaultPageNumber, Consts.defaultPageSize);
            }
        }

        public IPagedList<Order> GetOrders(string sortBy = "OrderTime", string sortOrder = "Descending", int page = 1, int limit = int.MaxValue)
        {
            var sortByProperty = typeof(Order).GetProperty(sortBy);
            var result = sortOrder == "Descending" ? _orders.OrderByDescending(o => sortByProperty.GetValue(o)).AsQueryable().Take(limit)
                                            : _orders.OrderBy(o => sortByProperty.GetValue(o)).AsQueryable().Take(limit);
            return result.ToPagedList(page, limit);
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