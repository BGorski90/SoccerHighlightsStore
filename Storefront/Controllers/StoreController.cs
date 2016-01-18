using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Web.Routing;
using Storefront.ViewModels;
using Common.Contracts;
using Storefront.Payments;
using Storefront.BusinessLayer.Repositories;
using Storefront.BusinessLayer.Entities;
using Common.Extensions;
using BusinessLayer.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Common.Infrastructure;
using Common.Cookies;

namespace Storefront.Controllers
{
    public class StoreController : Controller
    {
        private IVideoRepository _videosRepository;
        private IUserRepository _usersRepository;

        public StoreController(IVideoRepository repository)
        {
            _videosRepository = repository;
        }

        public StoreController(IVideoRepository repository, IUserRepository users)
        {
            _videosRepository = repository;
            _usersRepository = users;
        }

        public ActionResult Index(string category, string searchContent, string sortBy, string sortDirection, int? clipsPerPage, int? page)
        {
            SearchViewModel searchModel = new SearchViewModel
            {
                Category = category ?? "All",
                SearchContent = searchContent,
                SortBy = sortBy,
                SortDirection = sortDirection,
                ClipsPerPage = clipsPerPage ?? Consts.defaultPageSize,
                PageNumber = page ?? Consts.defaultPageNumber
            };

            if (category == null)
            {
                searchModel.Videos = _videosRepository.GetAll().ToPagedList(Consts.defaultPageNumber, searchModel.ClipsPerPage);
            }
            else
            {
                searchModel.Videos = GetVideos(searchModel).ToPagedList(searchModel.PageNumber, searchModel.ClipsPerPage);
            }

            searchModel.Cart = ExtractCartFromCookie();

            if (Request.IsAuthenticated)
            {
                var user = _usersRepository.FindByUsername(HttpContext.User.Identity.Name);
                if (user != null)
                {
                    searchModel.Wishlist = user.Wishlist.Select(v => v.VideoID);
                }
            }
            return View(searchModel);
        }

        //public ActionResult Search(SearchViewModel searchModel)
        //{
        //    searchModel.Videos = GetVideos(searchModel).ToPagedList(searchModel.CurrentPage, searchModel.ClipsPerPage);
        //    TempData["SearchModel"] = searchModel;
        //    return RedirectToAction("Index", new RouteValueDictionary() { { "category", searchModel.Category }, { "page", searchModel.CurrentPage } });
        //}

        private IQueryable<Video> GetVideos(SearchViewModel data)
        {
            var result = _videosRepository.Search(data.Category, data.SearchContent, data.SortBy, data.SortDirection == "desc" ? true : false);
            //return result.OrderBy<Video>(data.SortBy + " " + data.SortDirection);
            return result;
        }

        private Cart ExtractCartFromCookie()
        {
            var cookie = Request.Cookies[Consts.cartCookieName];
            if (cookie == null)
                return new Cart();
            return CookiesManager.ExtractCartFromCookie(cookie);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled && filterContext.Exception is HttpRequestValidationException)
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/Shared/Error.cshtml"
                };
                filterContext.ExceptionHandled = true;
            }
        }

    }
}
