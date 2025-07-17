using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IT_Project.Startup))]
namespace IT_Project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
