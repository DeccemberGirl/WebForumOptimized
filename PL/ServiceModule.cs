using AutoMapper;
using BLL;
using BLL.Builders;
using BLL.Builders.Interfaces;
using BLL.Services;
using BLL.Services.Interfaces;
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
            Bind<ITopicBuilder>().To<TopicBuilder>();
            Bind<IMessageDTOBuilder>().To<MessageDTOBuilder>();
            Bind<INewMessageFactory>().To<NewMessageFactory>();
            Bind<IMessageService>().To<MessageService>();
            Bind<ITopicService>().To<TopicService>();
            Bind<IUserService>().To<UserService>();
            Bind<IUserRoleService>().To<UserRoleService>();
            Bind<IAuthenticationManager>().ToMethod(c =>
            HttpContext.Current.GetOwinContext().Authentication).InRequestScope();
        }
    }
}