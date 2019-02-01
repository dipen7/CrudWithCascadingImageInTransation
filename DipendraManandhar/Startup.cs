using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DipendraManandhar.Startup))]
namespace DipendraManandhar
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
