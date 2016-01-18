using Common.Contracts;
using Common.Cookies;
using Common.Infrastructure;
using Storefront.BusinessLayer.Entities;
using Storefront.BusinessLayer.Repositories;
using Newtonsoft.Json;
using Storefront.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Storefront.Controllers
{
    public class CartController : Controller
    {
        private IVideoRepository _repository;

        public CartController(IVideoRepository repository)
        {
            _repository = repository;
        }
        public ActionResult AddToCart(Cart cart, int videoID)
        {
            cart.AddVideo(videoID);
            var newCookie = new HttpCookie("Cart", JsonConvert.SerializeObject(cart));
            Response.Cookies.Set(newCookie);

            if (Request.IsAjaxRequest())
                return Json("OK");
            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromCart(Cart cart, int videoID)
        {
            cart.RemoveVideo(videoID);
            var newCookie = new HttpCookie("Cart", JsonConvert.SerializeObject(cart));
            Response.Cookies.Set(newCookie);

            if (Request.IsAjaxRequest())
                return Json("OK");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ToggleCart(int videoID)
        {
            Cart cart = ExtractCartFromCookie();
            if (cart.Contains(videoID))
                return RemoveFromCart(cart, videoID);
            else
                return AddToCart(cart, videoID);
        }

        public ActionResult CheckCart()
        {
            Cart cart = ExtractCartFromCookie();
            var videos = _repository.GetAll().Where(v => cart.Videos.Contains(v.VideoID));
            return View("Cart", videos);
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