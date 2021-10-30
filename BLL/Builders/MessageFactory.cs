using BLL.Builders.Interfaces;
using BLL.Models;

namespace BLL.Builders
{
    /// <summary>
    /// NewMessage Factory for ViewModel of NewMessage
    /// </summary>
    public class NewMessageFactory : INewMessageFactory
    {
        public NewMessageFormModel Create(int topicId)
        {
            var message = new NewMessageFormModel { TopicId = topicId };
            return message;
        }
    }
}
