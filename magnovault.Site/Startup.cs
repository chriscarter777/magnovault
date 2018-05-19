using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(magnovault.Site.Startup))]
namespace magnovault.Site
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
