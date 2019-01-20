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
using Functional.Option;

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
            var cart = ExtractCartFromCookie();
            var videos = cart.HasValue ? _repository.GetCartVideos(cart.Value.Videos) : Enumerable.Empty<Video>();
            return View("Cart", videos);
        }

        private Option<Cart> ExtractCartFromCookie()
        {
            var cookie = Request.Cookies[Consts.cartCookieName];
            if (cookie == null)
                return Option<Cart>.None;
            return CookiesManager.ExtractCartFromCookie(cookie);
        }
    }
}