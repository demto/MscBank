using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MScBank.Startup))]
namespace MScBank
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
