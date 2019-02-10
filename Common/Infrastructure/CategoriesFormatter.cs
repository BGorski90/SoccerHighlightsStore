using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Common.Infrastructure
{
    public static class CategoriesFormatter
    {
        public static IEnumerable<SelectListItem> FormatCategories(IEnumerable<string> categories)
        {
            return categories.Select(cat => new SelectListItem { Text = cat, Value = cat });
        }

        public static IEnumerable<SelectListItem> FormatCategoriesForSearch(IEnumerable<string> categories)
        {
            yield return new SelectListItem { Text = "All", Value = "All" };

            foreach (var cat in categories)
            {
                yield return new SelectListItem { Text = cat, Value = cat };
            }
        }
    }
}
