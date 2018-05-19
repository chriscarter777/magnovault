using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(magnovault.Admin.Startup))]
namespace magnovault.Admin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
