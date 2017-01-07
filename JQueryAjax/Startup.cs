using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JQueryAjax.Startup))]
namespace JQueryAjax
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
