using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using SoccerHighlightsStore.DataAccessLayer.Repositories;
using SoccerHighlightsStore.BusinessLayer.Entities;
using SoccerHighlightsStore.Storefront.Areas.Admin.ViewModels;
using SoccerHighlightsStore.Common.Infrastructure;

namespace SoccerHighlightsStore.Storefront.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class HomeController : BaseController
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
            model.Videos = _videoRepository.Videos.Take(mainPageItems);
            model.Orders = _orderRepository.GetOrders(limit: mainPageItems);
            model.Users = _userRepository.GetUsers(limit: mainPageItems);
            return View(model);
        }

        public ActionResult Login()
        {
            return View();
        }
    }
}
