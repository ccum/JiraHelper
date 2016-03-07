using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ViewJira.Startup))]
namespace ViewJira
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
