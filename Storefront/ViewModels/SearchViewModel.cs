using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Storefront.BusinessLayer.Entities;
using Common.Infrastructure;

namespace Storefront.ViewModels
{
    public class SearchViewModel
    {
        public IPagedList<Video> Videos { get; set; }
        public string Category { get; set; }
        public int ClipsPerPage { get; set; }
        public string SortBy { get; set; }
        public string SortDirection { get; set; }
        [StringLength(100, ErrorMessage = "Search text too long")]
        [RegularExpression("[^<>]*", ErrorMessage = "HTML not allowed")]
        public string SearchContent { get; set; }
        public int PageNumber { get; set; }

        public Cart Cart { get; set; }
        public IEnumerable<int> Wishlist { get; set; } 

        public SearchViewModel()
        {
            Category = "All";
            ClipsPerPage = 1;
            SortBy = "Added";
            SortDirection = "Descending";
            SearchContent = "";
            PageNumber = 1;
        }
    }
}