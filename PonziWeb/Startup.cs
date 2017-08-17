using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PonziWeb.Startup))]
namespace PonziWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
