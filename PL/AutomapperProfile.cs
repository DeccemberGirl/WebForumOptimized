using AutoMapper;

namespace WebForum
{
    /// <summary>
    /// Automapper profile
    /// </summary>
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Message, MessageControl>()
                .ForMember(p => p.UserForumId, opt => opt.MapFrom(userId => userId.ForumUser.ForumProfile.Id))
                .ForMember(p => p.UserName, opt => opt.MapFrom(userName => userName.ForumUser.ForumProfile.Name))
                .IncludeMembers(x => x.ForumUser.ForumProfile).ReverseMap();
            CreateMap<ForumProfile, MessageControl>().ReverseMap();
            CreateMap<ForumProfile, TopicControl>().ReverseMap();
            CreateMap<Topic, TopicControl>()
                .ForMember(p => p.Name, opt => opt.MapFrom(name => name.Name))
                .ForMember(p => p.UserName, opt => opt.MapFrom(userName => userName.ForumUser.ForumProfile.Name))
                .ForMember(p => p.UserId, opt => opt.MapFrom(userId => userId.ForumUser.Id))
                .ForPath(p=>p.Messages.Messages, opt=>opt.MapFrom(messages=>messages.Messages))
                .ForMember(p => p.MessageCount, opt => opt.MapFrom(messages => messages.Messages.Count))
                .IncludeMembers(x => x.ForumUser.ForumProfile).ReverseMap().ForPath(x=>x.Messages, opt=>opt.MapFrom(x=>x.Messages.Messages));

            CreateMap<ForumUser, UserControl>()
                .ForMember(p => p.Id, opt => opt.MapFrom(userId => userId.Id))
                .ForMember(p => p.Email, opt => opt.MapFrom(email => email.Email))
                .ForMember(p => p.UserName, opt => opt.MapFrom(userName => userName.UserName));
                //.IncludeMembers(x => x.Roles.Select(x=>x.));
        }
    }
}
