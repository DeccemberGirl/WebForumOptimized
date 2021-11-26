using BLL.Models;

namespace BLL.Builders.Interfaces
{
    /// <summary>
    /// Factory to construct messages at the forum
    /// </summary>
    public interface INewMessageFactory
    {
        /// <summary>
        /// Creates an instance of the new message at the forum
        /// </summary>
        /// <param name="topicId">the Id of the topic corresponding to the message</param>
        /// <returns>Created NewMessageFormModel</returns>
        NewMessageFormModel Create(int topicId);
    }
}