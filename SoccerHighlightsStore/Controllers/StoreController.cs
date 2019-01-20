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
            var searchModel = CreateSearchModel(category, searchContent, sortBy, sortDirection, clipsPerPage, page);
            searchModel = FillWithVideos(searchModel, page);
            if (Request.IsAuthenticated)
            {
                searchModel = FillWithUserData(searchModel);
            }
            if (!Request.IsAjaxRequest())
                return View(searchModel);
            return Json(searchModel, JsonRequestBehavior.AllowGet);
        }

        private SearchViewModel FillWithUserData(SearchViewModel searchModel)
        {
            var user = _usersRepository.FindByUsername(User.Identity.Name);
            if (user != null)
            {
                searchModel.Username = user.UserName;
                searchModel.Wishlist = user.Wishlist.Select(v => v.VideoID);
            }

            return searchModel;
        }

        private SearchViewModel FillWithVideos(SearchViewModel searchModel, int? page)
        {
            _cacheManager = new VideoCacheManager(HttpContext, _videosRepository);
            if (page == null)
            {
                var cache = _cacheManager.Get("All");
                if (cache == null)
                {
                    var clips = _videosRepository.Videos;
                    searchModel.Videos = clips.ToPagedList(searchModel.PageNumber, searchModel.ClipsPerPage);
                }
                else
                {
                    searchModel.Videos = cache;
                }
            }
            else
            {
                var result = _cacheManager.Get(searchModel.Category);
                if (!string.IsNullOrWhiteSpace(searchModel.SearchContent) || result == null)
                {
                    result = _videosRepository.Search(
                        searchModel.Category,
                        searchModel.SearchContent,
                        searchModel.SortBy,
                        searchModel.SortDirection.Equals("Descending") ? true : false,
                        searchModel.PageNumber,
                        searchModel.ClipsPerPage).
                        ToPagedList(searchModel.PageNumber, searchModel.ClipsPerPage);
                }

                searchModel.Videos = result;
                searchModel.Cart = ExtractCartFromCookie();
            }

            return searchModel;
        }

        private SearchViewModel CreateSearchModel(string category, string searchContent, string sortBy, string sortDirection, int? clipsPerPage, int? page)
        {
            SearchViewModel searchModel = new SearchViewModel
            {
                Category = !string.IsNullOrWhiteSpace(category) ? category : "All",
                SearchContent = searchContent,
                SortBy = !string.IsNullOrWhiteSpace(sortBy) ? sortBy : "Added",
                SortDirection = !string.IsNullOrWhiteSpace(sortDirection) ? sortDirection : "Descending",
                ClipsPerPage = clipsPerPage ?? Consts.defaultPageSize,
                PageNumber = page ?? Consts.defaultPageNumber
            };

            if (searchModel.AvailableCategories == null || !searchModel.AvailableCategories.Any())
                searchModel.AvailableCategories = CategoriesFormatter.FormatCategories(_videosRepository.Categories);
            if (searchModel.SortProperties == null || !searchModel.AvailableCategories.Any())
                searchModel.SortProperties = SortPropertiesFormatter.FormatSortProperties();
            if (searchModel.SortDirectionOptions == null || !searchModel.SortDirectionOptions.Any())
                searchModel.SortDirectionOptions = new SelectListItem[] { new SelectListItem { Text = "Ascending", Value = "Ascending" }, new SelectListItem { Text = "Descending", Value = "Descending" } };
            return searchModel;
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
