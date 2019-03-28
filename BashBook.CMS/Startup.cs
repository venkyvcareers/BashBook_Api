using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BashBook.CMS.Startup))]
namespace BashBook.CMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
