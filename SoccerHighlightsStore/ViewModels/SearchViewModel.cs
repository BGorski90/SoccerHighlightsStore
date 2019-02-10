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
    public class SearchViewModel
    {
        public IEnumerable<SelectListItem> SortProperties { get; set; }
        public IEnumerable<SelectListItem> SortDirectionOptions { get; set; }
        public string Category { get; set; }
        public string SortBy { get; set; }
        public string SortDirection { get; set; }
        [StringLength(100, ErrorMessage = "Search text too long")]
        [RegularExpression("[^<>]*", ErrorMessage = "HTML not allowed")]
        public string SearchContent { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        private SearchViewModel(
            string category,
            string searchContent,
            string sortBy,
            string sortDirection,
            int pageNumber,
            int pageSize,
            IEnumerable<SelectListItem> sortProperties,
            IEnumerable<SelectListItem> sortDirectionOptions)
        {
            Category = category;
            SearchContent = searchContent;
            SortBy = sortBy;
            SortDirection = sortDirection;
            PageNumber = pageNumber;
            PageSize = pageSize;
            SortProperties = sortProperties;
            SortDirectionOptions = sortDirectionOptions;
        }

        public static SearchViewModel Create(string category,
            string searchContent,
            string sortBy,
            string sortDirection,
            int? pageNumber,
            int? pageSize)
        {
            return new SearchViewModel(
                !string.IsNullOrWhiteSpace(category) ? category : "All",
                searchContent,
                !string.IsNullOrWhiteSpace(sortBy) ? sortBy : "Added",
                !string.IsNullOrWhiteSpace(sortDirection) ? sortDirection : "Descending",
                pageSize ?? Consts.defaultPageSize,
                pageNumber ?? Consts.defaultPageNumber,
                SortPropertiesFormatter.FormatSortProperties(),
                new SelectListItem[]
                {
                    new SelectListItem {
                        Text = "Ascending", Value = "Ascending"
                    },
                    new SelectListItem {
                        Text = "Descending", Value = "Descending"
                    }
                }); 
        }
    }
}