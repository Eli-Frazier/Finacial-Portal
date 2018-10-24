using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Finacial_Portal.Startup))]
namespace Finacial_Portal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
