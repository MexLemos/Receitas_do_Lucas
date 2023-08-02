using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(receitando.Startup))]
namespace receitando
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
