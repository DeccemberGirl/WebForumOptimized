using DAL.Entities;
using DAL.Repositories.Interfaces;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    /// <summary>
    /// Message repository
    /// </summary>
    public class MessageRepository : IMessageRepository
    {
        readonly private ForumContext _context;

        /// <summary>
        /// Creates a new instance of the <see cref="MessageRepository" />
        /// </summary>
        /// <param name="context">Forum context object</param>
        public MessageRepository(ForumContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds message to the repository 
        /// </summary>
        /// <param name="entity">Message object</param>
        /// <returns>Message Id</returns>
        public async Task<int> AddAsync(Message entity)
        {
            _context.Messages.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        /// <summary>
        /// Adds message to topic 
        /// </summary>
        /// <param name="entity">Message object</param>
        /// <param name="id">Topic Id</param>
        /// <returns>Message Id if the operation succeded</returns>
        public async Task<int> AddAsyncMessageToTopic(Message entity, int id)
        {
            var topic = _context.Topics.First(x => x.Id == id);
            entity.Topic = topic;
            _context.Messages.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        /// <summary>
        /// Deletes the message from the repository
        /// </summary>
        /// <param name="entity">Message object</param>
        public void Delete(Message entity)
        {
            _context.Messages.Remove(entity);
        }

        /// <summary>
        /// Deletes the message from the repository by its id
        /// </summary>
        /// <param name="id">Message Id</param>
        public async Task DeleteByIdAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            _context.Messages.Remove(entity);
        }

        /// <summary>
        /// Deletes all messages from the user
        /// </summary>
        /// <param name="userId">User Id</param>
        public void DeleteAllUserMessages(string userId)
        {
            var userMessages = _context.Messages.Where(x => x.ForumUser.Id == userId);
            _context.Messages.RemoveRange(userMessages);
            _context.SaveChanges();
        }

        /// <summary>
        /// Gets all objects of the collection from the repository
        /// </summary>
        /// <returns>Collection of messages</returns>
        public IQueryable<Message> FindAll()
        {
            _context.Messages.Include("ForumUser");
            _context.Messages.Include(x => x.Topic);
            return _context.Messages;
        }

        /// <summary>
        /// Gets the concrete message from the repository by its id
        /// </summary>
        /// <param name="id">Entity Id</param>
        /// <returns>Collection of messages</returns>
        public async Task<Message> GetByIdAsync(int id)
        {
            return await _context.Messages.SingleAsync(x => x.Id == id);
        }

        /// <summary>
        /// Updates the message info in the repository
        /// </summary>
        /// <param name="entity">Message object</param>
        public async Task Update(Message entity)
        {
            var foundEntity =  await GetByIdAsync(entity.Id);
            foundEntity.Text = entity.Text;
            _context.SaveChanges();
        }
    }
}
