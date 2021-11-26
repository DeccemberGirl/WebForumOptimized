using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(WebForum.App_Start.Startup))]
namespace WebForum.App_Start
{
    /// <summary>
    /// Start class of the whole programm, the point of the main entry
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configures the mvc application
        /// </summary>
        /// <param name="app">AppBuilder entity</param>
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });           
        }
    }
}