using DAL.Entities;
using DAL.Identity;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// Unit of work, which is the point of connection to DAL
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Forum context instance
        /// </summary>
        private readonly ForumContext _db;

        /// <summary>
        /// Users Manager of the forum
        /// </summary>
        private readonly ForumUserManager _userManager;

        /// <summary>
        /// User roles manager of the forum
        /// </summary>
        private readonly ForumRoleManager _roleManager;

        /// <summary>
        /// User profiles manager of the forum
        /// </summary>
        private readonly IForumProfileManager _forumProfileManager;

        /// <summary>
        /// Messages repository object
        /// </summary>
        private readonly IMessageRepository _messageRepository;

        /// <summary>
        /// Topics repository object
        /// </summary>
        private readonly ITopicRepository _topicRepository;

        /// <summary>
        /// Creates a new instance of the <see cref="UnitOfWork" />
        /// </summary>
        public UnitOfWork()
        {
            _db = new ForumContext();
            
        }

        /// <summary>
        /// Users Manager of the forum
        /// </summary>
        public ForumUserManager UserManager => _userManager ?? new ForumUserManager(new UserStore<ForumUser>(_db));

        /// <summary>
        /// User roles manager of the forum
        /// </summary>
        public ForumRoleManager RoleManager => _roleManager ?? new ForumRoleManager(new RoleStore<ForumRole>(_db));

        /// <summary>
        /// User profiles manager of the forum
        /// </summary>
        public IForumProfileManager ClientManager => _forumProfileManager??new ForumProfileManager(_db);

        /// <summary>
        /// Messages repository object
        /// </summary>
        public IMessageRepository MessageRepository => _messageRepository?? new MessageRepository(_db);

        /// <summary>
        /// Topics repository object
        /// </summary>
        public ITopicRepository TopicRepository => _topicRepository??new TopicRepository(_db);

        /// <summary>
        /// Unit of work disposing
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _userManager.Dispose();
                    _roleManager.Dispose();
                    _forumProfileManager.Dispose();
                    
                }
                disposed = true;
            }
        }

        /// <summary>
        /// Saves changes
        /// </summary>
        /// <returns></returns>
        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();           
        }
    }
}
