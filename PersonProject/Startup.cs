using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PersonProject.Startup))]
namespace PersonProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
