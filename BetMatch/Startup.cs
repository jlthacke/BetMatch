using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BetMatch.Startup))]
namespace BetMatch
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
