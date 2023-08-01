using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Owin;
using WhiteBoardDemo.Services;

[assembly: OwinStartupAttribute(typeof(WhiteBoardDemo.Startup))]
namespace WhiteBoardDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    
    }
}
