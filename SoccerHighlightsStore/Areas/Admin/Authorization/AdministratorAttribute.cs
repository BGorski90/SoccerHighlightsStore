using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoccerHighlightsStore.Storefront.Areas.Admin.Authorization
{
    public class AdministratorAttribute : AuthorizeAttribute
    {
        private readonly string loginUrl = "/Admin/Home/Login";

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.HttpContext.Response.Redirect(loginUrl);
            }
            base.OnAuthorization(filterContext);
        }
    }
}