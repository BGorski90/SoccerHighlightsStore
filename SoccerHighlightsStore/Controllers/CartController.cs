using SoccerHighlightsStore.Common.Contracts;
using SoccerHighlightsStore.Common.Cookies;
using SoccerHighlightsStore.Common.Infrastructure;
using SoccerHighlightsStore.BusinessLayer.Entities;
using SoccerHighlightsStore.DataAccessLayer.Repositories;
using Newtonsoft.Json;
using SoccerHighlightsStore.Storefront.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoccerHighlightsStore.Storefront.Controllers
{
    public class CartController : BaseController
    {
        private IVideoRepository _repository;

        public CartController(IVideoRepository repository)
        {
            _repository = repository;
        }

        public ActionResult CheckCart()
        {
            Cart cart = ExtractCartFromCookie();
            var videos = cart != null ? _repository.GetCartVideos(cart.Videos) : null;
            return View("Cart", videos);
        }

        private Cart ExtractCartFromCookie()
        {
            var cookie = Request.Cookies[Consts.cartCookieName];
            if (cookie == null)
                return null;
            return CookiesManager.ExtractCartFromCookie(cookie);
        }
    }
}