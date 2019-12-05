using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Work_01.Startup))]
namespace Work_01
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
