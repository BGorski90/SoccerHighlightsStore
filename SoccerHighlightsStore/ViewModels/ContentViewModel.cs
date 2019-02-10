using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using SoccerHighlightsStore.BusinessLayer.Entities;
using SoccerHighlightsStore.Common.Infrastructure;
using PagedList;
using SoccerHighlightsStore.Common.Contracts;
using Common.Infrastructure;

namespace SoccerHighlightsStore.Storefront.ViewModels
{
    public class ContentViewModel
    {
        public IPagedList<Video> Videos { get; set; }
        public IEnumerable<SelectListItem> AvailableCategories { get; set; }

        private ContentViewModel(
            IPagedList<Video> videos, 
            IEnumerable<SelectListItem> categories)
        {
            Videos = videos;
            AvailableCategories = categories;
        }

        public static ContentViewModel Create(
            IEnumerable<Video> videos, 
            IEnumerable<string> categories,
            int? pageNumber,
            int? pageSize)
        {
            return new ContentViewModel(
                videos.ToPagedList(
                    pageSize ?? Consts.defaultPageSize,
                    pageNumber ?? Consts.defaultPageNumber),
                CategoriesFormatter.FormatCategories(categories));
        }
    }
}