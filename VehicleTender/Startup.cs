using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VehicleTender.Startup))]
namespace VehicleTender
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
