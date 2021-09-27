using AutoMapper;
using Microsoft.Owin.Security;
using Ninject.Modules;
using Ninject.Web.Common;
using System.Web;

namespace WebForum
{
    /// <summary>
    /// Ninject model for registering all dependencies
    /// </summary>
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IMapper>().ToMethod(context =>
                {
                    var config = new MapperConfiguration( mc => { mc.AddProfile(new AutomapperProfile()); });
                    return config.CreateMapper();
                }).InSingletonScope();
            Bind<IAuthenticationManager>().ToMethod(c =>
            HttpContext.Current.GetOwinContext().Authentication).InRequestScope();
        }
    }
}