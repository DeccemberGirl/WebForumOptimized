using BLL.Models;

namespace BLL.Builders.Interfaces
{
    public interface INewMessageFactory
    {
        NewMessageFormModel Create(int topicId);
    }
}