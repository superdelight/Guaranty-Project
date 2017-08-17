using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GuarantyAdmin.Startup))]
namespace GuarantyAdmin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
