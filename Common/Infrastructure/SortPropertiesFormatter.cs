using SoccerHighlightsStore.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Common.Infrastructure
{
    public static class SortPropertiesFormatter
    {
        public static IEnumerable<SelectListItem> FormatSortProperties()
        {
            return Consts.sortProperties.
                Select( prop => new SelectListItem { Text = prop, Value = prop });
        }
    }
}