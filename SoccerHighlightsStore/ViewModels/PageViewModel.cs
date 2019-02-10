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
using Functional.Option;

namespace SoccerHighlightsStore.Storefront.ViewModels
{
    public class PageViewModel
    {
        private readonly SearchViewModel searchViewModel;
        private readonly ContentViewModel contentViewModel;
        private readonly UserViewModel userViewModel;

        private PageViewModel(
            SearchViewModel searchViewModel,
            ContentViewModel contentViewModel,
            UserViewModel userViewModel)
        {
            this.searchViewModel = searchViewModel;
            this.contentViewModel = contentViewModel;
            this.userViewModel = userViewModel;
        }

        public static PageViewModel Create(
            SearchViewModel searchViewModel,
            ContentViewModel contentViewModel,
            UserViewModel userViewModel)
        {
            return new PageViewModel(
                searchViewModel,
                contentViewModel,
                userViewModel);
        }
    }
}