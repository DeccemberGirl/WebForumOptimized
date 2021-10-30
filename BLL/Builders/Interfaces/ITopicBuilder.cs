using BLL.DTO;

namespace BLL.Builders.Interfaces
{
    public interface ITopicBuilder
    {
        TopicDTO CreateTopicDTO(string id, string userName, string name, string text);
    }
}