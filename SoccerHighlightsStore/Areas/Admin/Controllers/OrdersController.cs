using SoccerHighlightsStore.Common.Contracts;
using SoccerHighlightsStore.DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SoccerHighlightsStore.Common.Infrastructure;
using PagedList;

namespace SoccerHighlightsStore.Storefront.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class OrdersController : BaseController
    {
        private IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository repo)
        {
            _orderRepository = repo;
        }

        // GET: Admin/Orders
        public ActionResult Index()
        {
            var model = _orderRepository.Orders;
            return View(model);
        }
    }
}