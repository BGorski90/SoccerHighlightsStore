using Common.Contracts;
using Storefront.BusinessLayer.Repositories;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Storefront.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class OrdersController : Controller
    {
        private IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository repo)
        {
            _orderRepository = repo;
        }

        // GET: Admin/Orders
        public ActionResult Index()
        {
            var model = _orderRepository.GetAll().ToPagedList(Consts.defaultPageNumber, Consts.defaultPageSize);
            return View(model);
        }
    }
}