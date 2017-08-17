using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GuarantyWeb.Startup))]
namespace GuarantyWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
