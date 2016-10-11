using SevenZip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SoccerHighlightsStore.Storefront
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //GlobalFilters.Filters.Add(new RequireHttpsAttribute());
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutofacConfig.RegisterTypes();
            MvcHandler.DisableMvcResponseHeader = true;
            SevenZipExtractor.SetLibraryPath(@"C:\Users\Bartek\Documents\Visual Studio 2015\Projects\SoccerHighlightsStore\SoccerHighlightsStore\bin\7z.dll");
        }

        protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
        {
            HttpApplication app = sender as HttpApplication;
            
            if (app != null && app.Context != null)
            {
                app.Context.Response.Headers.Remove("Server");
            }
        }

        protected void Application_Error()
        {
            var error = Server.GetLastError();
            Server.ClearError();
            Response.Redirect("/Error/PageNotFound");
        }
    }
}
