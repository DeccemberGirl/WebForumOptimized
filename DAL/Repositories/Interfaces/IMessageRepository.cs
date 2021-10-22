using DAL.Entities;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    /// <summary>
    /// Message repository interface
    /// </summary>
    public interface IMessageRepository : IRepository<Message>
    {
        /// <summary>
        /// Adds message to topic 
        /// </summary>
        /// <param name="entity">Entity object</param>
        /// <param name="id">Entity Id</param>
        /// <returns>Entity Id if the operation succeded</returns>
        Task<int> AddAsyncMessageToTopic(Message entity, int id);

        /// <summary>
        /// Deletes all messages from the user
        /// </summary>
        /// <param name="userId">User Id</param>
        void DeleteAllUserMessages(string userId);
    }
}
