using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Storefront.BusinessLayer.Repositories;
using Storefront.BusinessLayer.Entities;
using Storefront.Areas.Admin.ViewModels;

namespace Storefront.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class HomeController : Controller
    {
        private IVideoRepository _videoRepository;
        private IOrderRepository _orderRepository;
        private IUserRepository _userRepository;

        private readonly int mainPageItems = 3;

        public HomeController() { }

        public HomeController(IVideoRepository repository, IOrderRepository orders, IUserRepository users)
        {
            _videoRepository = repository;
            _orderRepository = orders;
            _userRepository = users;
        }

        public ActionResult Index()
        {
            var model = new AdminHomeViewModel();
            model.Videos = _videoRepository.GetAll(limit: mainPageItems);
            model.Orders = _orderRepository.GetAll(limit: mainPageItems);
            model.Users = _userRepository.GetAll(limit: mainPageItems);
            return View(model);
        }

        public ActionResult Login()
        {
            return View();
        }
    }
}
