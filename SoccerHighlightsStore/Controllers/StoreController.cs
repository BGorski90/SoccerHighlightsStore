using System.Linq;
using System.Web.Mvc;
using PagedList;
using SoccerHighlightsStore.Storefront.ViewModels;
using SoccerHighlightsStore.Common.Contracts;
using SoccerHighlightsStore.DataAccessLayer.Repositories;
using SoccerHighlightsStore.BusinessLayer.Entities;
using SoccerHighlightsStore.Common.Infrastructure;
using SoccerHighlightsStore.Common.Cookies;
using Common.Infrastructure;
using BusinessLayer.Helpers;

namespace SoccerHighlightsStore.Storefront.Controllers
{
    public class StoreController : BaseController
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

        public ActionResult Index(string category, string searchContent, string sortBy, string sortDirection, int? clipsPerPage, int? page, bool? includeTotal)
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
            if (page == null)
            {
                searchModel.Videos = _videosRepository.Videos;
                searchModel.TotalVideos = _videosRepository.TotalClips;
            }
            else
            {
                var result = _videosRepository.
                    Search(searchModel.Category, searchModel.SearchContent, searchModel.SortBy,
                    searchModel.SortDirection.Equals("Descending") ? true : false, searchModel.PageNumber,
                    searchModel.ClipsPerPage, includeTotal ?? true);
                searchModel.Videos = result.Videos;
                searchModel.TotalVideos = result.TotalVideos;
            }

            searchModel.Cart = ExtractCartFromCookie();

            if (Request.IsAuthenticated)
            {
                var user = _usersRepository.FindByUsername(User.Identity.Name);
                if (user != null)
                {
                    searchModel.Username = user.UserName;
                    searchModel.Wishlist = user.Wishlist.Select(v => v.VideoID);
                }
            }
            if (!Request.IsAjaxRequest())
                return View(searchModel);
            return Json(searchModel, JsonRequestBehavior.AllowGet);
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
