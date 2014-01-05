using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GPA.Startup))]
namespace GPA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
