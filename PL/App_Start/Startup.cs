using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
[assembly: OwinStartup(typeof(WebForum.App_Start.Startup))]
namespace WebForum.App_Start
{
    public class Startup
    {

        
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