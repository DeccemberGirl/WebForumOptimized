using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Threading.Tasks;

namespace WebForum
{
    /// <summary>
    /// Unit of work, wich is point of connection to DAL
    /// </summary>
    public class UnitOfWork
    {
        private readonly ForumContext _db;
        private readonly ForumUserManager _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ForumProfileManager _forumProfileManager;
        private readonly MessageRepository _messageRepository;
        private readonly TopicRepository _topicRepository;
        public UnitOfWork()
        {
            _db = new ForumContext();
            
        }
        public ForumUserManager UserManager => _userManager ?? new ForumUserManager(new UserStore<ForumUser>(_db));
        public RoleManager<IdentityRole> RoleManager => _roleManager ?? new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_db));

        public ForumProfileManager ClientManager => _forumProfileManager??new ForumProfileManager(_db);

        public MessageRepository MessageRepository => _messageRepository?? new MessageRepository(_db);

        public TopicRepository TopicRepository => _topicRepository??new TopicRepository(_db);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _userManager.Dispose();
                    _roleManager.Dispose();
                    _forumProfileManager.Dispose();
                    
                }
                this.disposed = true;
            }
        }
        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
            
        }
    }
}
