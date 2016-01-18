using Common.Infrastructure;
using Storefront.BusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storefront.BusinessLayer.Repositories
{
    public interface IOrderRepository
    {
        IQueryable<Order> GetAll(string sortBy = "OrderTime", string sortOrder = "desc", int limit = int.MaxValue);
        Order Get(int id);
        void CreateOrder(Cart cart, string userId);
        void Add(Order order);
        void AddOrderInfoToUser(Order order);
    }
}
