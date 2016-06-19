using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Common.Infrastructure
{
    public static class CategoriesFormatter
    {
        public static IEnumerable<SelectListItem> FormatCategories(IEnumerable<string> categories, bool search = true)
        {
            var result = new List<SelectListItem>();
            if(search)
                result.Add(new SelectListItem { Text = "All", Value = "All" });
            foreach(var cat in categories)
            {
                result.Add(new SelectListItem { Text = cat, Value = cat });
            }
            return result;
        }
    }
}
