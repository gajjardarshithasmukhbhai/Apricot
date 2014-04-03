using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Apricot.Web.Startup))]
namespace Apricot.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
