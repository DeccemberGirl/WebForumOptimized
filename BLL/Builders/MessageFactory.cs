using BLL.Builders.Interfaces;
using BLL.Models;

namespace BLL.Builders
{
    /// <summary>
    /// NewMessage Factory for ViewModel of NewMessage
    /// </summary>
    public class NewMessageFactory : INewMessageFactory
    {
        /// <summary>
        /// Creates an instance of the new message at the forum
        /// </summary>
        /// <param name="topicId">the Id of the topic corresponding to the message</param>
        public NewMessageFormModel Create(int topicId)
        {
            return new NewMessageFormModel { TopicId = topicId };
        }
    }
}
