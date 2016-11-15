using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TuLista.Startup))]
namespace TuLista
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
