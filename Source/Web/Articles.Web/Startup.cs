using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Articles.Web.Startup))]
namespace Articles.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
