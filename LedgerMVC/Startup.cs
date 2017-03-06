using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LedgerMVC.Startup))]
namespace LedgerMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
