using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ReservationFinal.MVC.UI.Startup))]
namespace ReservationFinal.MVC.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
