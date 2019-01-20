using SoccerHighlightsStore.Common.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Functional.Option;

namespace SoccerHighlightsStore.Common.Cookies
{
    public static class CookiesManager
    {
        public static Option<Cart> ExtractCartOrEmptyFromCookie(HttpCookie cookie)
        {
            Cart cart = JsonConvert.DeserializeObject<Cart>(cookie.Value);
            if (cart == null)
                return Option<Cart>.None;
            return Option<Cart>.Some(cart);
        }

        public static Cart ExtractCartFromCookie(HttpCookie cookie)
        {
            return JsonConvert.DeserializeObject<Cart>(cookie.Value);
        }
    }
}
