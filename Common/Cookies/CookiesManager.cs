using SoccerHighlightsStore.Common.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SoccerHighlightsStore.Common.Cookies
{
    public static class CookiesManager
    {
        public static Cart ExtractCartFromCookie(HttpCookie cookie)
        {
            if (cookie == null)
                throw new ArgumentNullException("cookie");
            Cart cart = JsonConvert.DeserializeObject<Cart>(cookie.Value);
            if (cart == null)
                cart = new Cart();
            return cart;
        }
    }
}
