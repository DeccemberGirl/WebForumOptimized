using AutoMapper;
using BLL.DTO;
using DAL.Entities;

namespace BLL
{
    /// <summary>
    /// Automapper profile
    /// </summary>
    public class AutomapperProfile : Profile
    {
        /// <summary>
        /// Creates an instanse of an <see cref="AutomapperProfile">class</see>
        /// </summary>
        public AutomapperProfile()
        {
            CreateMap<Message, MessageDTO>()
                .ForMember(p => p.UserForumId, opt => opt.MapFrom(userId => userId.ForumUser.ForumProfile.Id))
                .ForMember(p => p.UserName, opt => opt.MapFrom(userName => userName.ForumUser.ForumProfile.Name))
                .IncludeMembers(x => x.ForumUser.ForumProfile).ReverseMap();
            CreateMap<ForumProfile, MessageDTO>().ReverseMap();
            CreateMap<ForumProfile, TopicDTO>().ReverseMap();
            CreateMap<Topic, TopicDTO>()
                .ForMember(p => p.Name, opt => opt.MapFrom(name => name.Name))
                .ForMember(p => p.UserName, opt => opt.MapFrom(userName => userName.ForumUser.ForumProfile.Name))
                .ForMember(p => p.UserId, opt => opt.MapFrom(userId => userId.ForumUser.Id))
                .ForPath(p=>p.Messages.Messages, opt=>opt.MapFrom(messages=>messages.Messages))
                .ForMember(p => p.MessageCount, opt => opt.MapFrom(messages => messages.Messages.Count))
                .IncludeMembers(x => x.ForumUser.ForumProfile).ReverseMap().ForPath(x=>x.Messages, opt=>opt.MapFrom(x=>x.Messages.Messages));

            CreateMap<ForumUser, UserDTO>()
                .ForMember(p => p.Id, opt => opt.MapFrom(userId => userId.Id))
                .ForMember(p => p.Email, opt => opt.MapFrom(email => email.Email))
                .ForMember(p => p.UserName, opt => opt.MapFrom(userName => userName.UserName));
        }
    }
}
