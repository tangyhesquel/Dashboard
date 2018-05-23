using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcDashBoard.WebUI.Startup))]
namespace MvcDashBoard.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
