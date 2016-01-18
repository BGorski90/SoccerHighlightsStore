using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Storefront.Startup))]
namespace Storefront
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
