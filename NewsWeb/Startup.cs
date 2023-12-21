using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(NewsWeb.StartupOwin))]

namespace NewsWeb
{
    public partial class StartupOwin
    {
        public void Configuration(IAppBuilder app)
        {
            //AuthStartup.ConfigureAuth(app);
        }
    }
}
