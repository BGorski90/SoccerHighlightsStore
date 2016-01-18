using Common.Contracts;
using Common.Cookies;
using Common.Infrastructure;
using Storefront.BusinessLayer.Entities;
using Storefront.BusinessLayer.Repositories;
using Storefront.Payments;
using Storefront.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Storefront.Controllers
{
    public class OrderController : Controller
    {
        private IVideoRepository _videosRepository;
        private IOrderRepository _ordersRepository;
        private IUserRepository _usersRepository;
        private IPaymentProcessor _paymentProcessor;

        public OrderController(IVideoRepository videos, IOrderRepository orders,
                                IUserRepository users, IPaymentProcessor processor)
        {
            _videosRepository = videos;
            _usersRepository = users;
            _ordersRepository = orders;
            _paymentProcessor = processor;
        }

        [HttpGet]
        public ActionResult Checkout()
        {
            Cart cart = ExtractCartFromCookie();
            var videos = _videosRepository.GetAll().Where(v => cart.Videos.Contains(v.VideoID));
            var value = videos.Sum(v => v.Price);
            return View(new PaymentViewModel { Cart = videos, OrderValue = value });
        }

        [HttpPost]
        public RedirectToRouteResult Checkout(PaymentViewModel paymentData)
        {
            //Authorize payment
            if (_paymentProcessor.AuthorizePayment(paymentData))
            {
                Cart cart = ExtractCartFromCookie();
                var userID = User.Identity.IsAuthenticated ?
                                            _usersRepository.FindByEmail(paymentData.EmailAddress).Id
                                            : Consts.anonymousUserID;
                _ordersRepository.CreateOrder(cart, userID);
                Response.Cookies.Remove(Consts.cartCookieName);
            }
            else
            {
                //display error
            }
            return RedirectToRoute("Default", new { controller = "Store", action = "Index" });
        }

        private Cart ExtractCartFromCookie()
        {
            var cookie = Request.Cookies[Consts.cartCookieName];
            if (cookie == null)
                return new Cart();
            return CookiesManager.ExtractCartFromCookie(cookie);
        }
    }
}