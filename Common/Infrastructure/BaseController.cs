using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SoccerHighlightsStore.Common.Infrastructure
{
    public class BaseController : Controller
    {
        protected override void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                if (!filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new ViewResult
                    {
                        ViewName = "~/Views/Shared/Error.cshtml"
                    };
                }
                else
                {
                    filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                filterContext.ExceptionHandled = true;
            }
        }
    }
}
