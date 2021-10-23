using DAL.Identity;
using DAL.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// Unit of work interface
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Users Manager of the forum
        /// </summary>
        ForumUserManager UserManager { get; }

        /// <summary>
        /// User profiles manager of the forum
        /// </summary>
        IForumProfileManager ClientManager { get; }

        /// <summary>
        /// User roles manager of the forum
        /// </summary>
        ForumRoleManager RoleManager { get; }

        /// <summary>
        /// Messages repository object
        /// </summary>
        IMessageRepository MessageRepository { get; }

        /// <summary>
        /// Topics repository object
        /// </summary>
        ITopicRepository TopicRepository { get; }

        /// <summary>
        /// Saves changes 
        /// </summary>
        /// <returns></returns>
        Task SaveAsync();
    }
}
