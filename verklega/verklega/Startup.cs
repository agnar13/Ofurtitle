using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(verklega.Startup))]
namespace verklega
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
