using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SoccerHighlightsStore.Storefront.Startup))]
namespace SoccerHighlightsStore.Storefront
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
