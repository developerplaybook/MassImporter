using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Importer.Site.Startup))]
namespace Importer.Site
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
