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
            var result = new List<SelectListItem>();
            foreach (var prop in Consts.sortProperties)
            {
                result.Add(new SelectListItem { Text = prop, Value = prop });
            }
            return result;
        }
    }
}
