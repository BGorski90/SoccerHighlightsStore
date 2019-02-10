using System.Linq;
using System.Web.Mvc;
using SoccerHighlightsStore.Storefront.ViewModels;
using SoccerHighlightsStore.Common.Contracts;
using SoccerHighlightsStore.DataAccessLayer.Repositories;
using SoccerHighlightsStore.Common.Infrastructure;
using SoccerHighlightsStore.Common.Cookies;
using Common.Infrastructure;
using DataAccessLayer.Helpers;
using PagedList;
using SoccerHighlightsStore.BusinessLayer.Entities;
using System.Collections.Generic;

namespace SoccerHighlightsStore.Storefront.Controllers
{
    public class StoreController : BaseController
    {
        private IVideoRepository _videosRepository;
        private IUserRepository _usersRepository;
        private VideoCacheManager _cacheManager;

        public StoreController(IVideoRepository repository, IUserRepository users)
        {
            _videosRepository = repository;
            _usersRepository = users;
        }

        public ActionResult Index(string category, string searchContent, string sortBy, string sortDirection, int? clipsPerPage, int? page)
        {
            var searchModel = SearchViewModel.Create(
                category, searchContent, sortBy, sortDirection, clipsPerPage, page);

            var contentModel = ContentViewModel.Create(
                page.HasValue ? GetFilteredVideos(searchModel) 
                                : GetAllVideos(),
                _videosRepository.Categories,
                page,
                clipsPerPage);

            var userModel = UserViewModel.Empty();

            if (Request.IsAuthenticated)
            {
                var user = _usersRepository.FindByUsername(User.Identity.Name);
                userModel = UserViewModel.Create(
                        user.Value.UserName,
                        ExtractCartFromCookie(),
                        user.Value.Wishlist.Select(v => v.VideoID)
                    );
            }

            var pageViewModel = PageViewModel.Create(
                searchModel,
                contentModel,
                userModel);

            if (Request.IsAjaxRequest())
                return Json(searchModel, JsonRequestBehavior.AllowGet);

            return View(pageViewModel);
        }

        private IEnumerable<Video> GetAllVideos()
        {
            _cacheManager = new VideoCacheManager(HttpContext, _videosRepository);
            return _cacheManager.Get("All") ?? _videosRepository.Videos;
        }

        private IEnumerable<Video> GetFilteredVideos(SearchViewModel searchModel)
        {
            _cacheManager = new VideoCacheManager(HttpContext, _videosRepository);

            var result = _cacheManager.Get(searchModel.Category);

            if (result == null || !string.IsNullOrWhiteSpace(searchModel.SearchContent))
            {
                result = _videosRepository.Search(
                    searchModel.Category,
                    searchModel.SearchContent,
                    searchModel.SortBy,
                    searchModel.SortDirection.Equals("Descending") ? true : false,
                    searchModel.PageNumber,
                    searchModel.PageSize);                
            }

            return result;
        }

    private Cart ExtractCartFromCookie()
    {
        var cookie = Request.Cookies[Consts.cartCookieName];
        if (cookie == null)
            return new Cart(Enumerable.Empty<int>());
        return CookiesManager.ExtractCartFromCookie(cookie);
    }
}
}
