using SoccerHighlightsStore.Common.Infrastructure;
using SoccerHighlightsStore.BusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace SoccerHighlightsStore.DataAccessLayer.Repositories
{
    public interface IOrderRepository
    {
        IEnumerable<Order> Orders { get; }
        IEnumerable<Order> GetOrders(string sortBy = "OrderTime", int limit = int.MaxValue);
        Order Get(int id);
        void CreateOrder(Cart cart, string userId);
        void Add(Order order);
        void AddOrderInfoToUser(Order order);
    }
}
