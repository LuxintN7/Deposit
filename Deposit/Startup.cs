using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Deposit.Startup))]
namespace Deposit
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //app.MapSignalR();
        }
    }
}

