using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SistemZaZakazuvanje.Startup))]
namespace SistemZaZakazuvanje
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
