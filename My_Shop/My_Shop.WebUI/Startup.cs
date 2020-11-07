using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(My_Shop.WebUI.Startup))]
namespace My_Shop.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
