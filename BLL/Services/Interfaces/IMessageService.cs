using BLL.DTO;
using BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    /// <summary>
    /// Service to handle message entities
    /// </summary>
    public interface IMessageService
    {
        /// <summary>
        /// Adds a new message to db asynchronously
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <param name="userName">Name of the author</param>
        /// <param name="text">Text of the message</param>
        /// <param name="topicId">Id of the topic</param>
        Task AddAsync(string userId, string userName, string text, int topicId);

        /// <summary>
        /// Deletes the message from db asynchronously
        /// </summary>
        /// <param name="messageId">Id of the message</param>
        Task DeleteByIdAsync(int messageId);

        /// <summary>
        /// Updates the message asynchronously
        /// </summary>
        /// <param name="message">Message DTO</param>
        Task UpdateAsync(MessageDTO message);

        /// <summary>
        /// Returns all the messages from db
        /// </summary>
        /// <returns>The collection of message DTO-s</returns>
        IEnumerable<MessageDTO> GetAll();

        /// <summary>
        /// Returns the page of the specified number with the specified messages
        /// </summary>
        /// <param name="messages">The collection of messages dto-s</param>
        /// <param name="pageNumber">The number of the page</param>
        /// <returns>Model of the page with messages</returns>
        PagedMessagesModel GetPagedMessages(IEnumerable<MessageDTO> messages, int pageNumber);
    }
}