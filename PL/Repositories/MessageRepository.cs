
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebForum
{
    /// <summary>
    /// Message repository
    /// </summary>
    public class MessageRepository
    {
        readonly private ForumContext _context;
        public MessageRepository(ForumContext context)
        {
            _context = context;
        }
        public async Task<int> AddAsync(Message entity)
        {
            _context.Messages.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;

        }
        /// <summary>
        /// Add message entity to specified topic
        /// </summary>
        /// <param name="entity"> Message entity </param>
        /// <param name="id"> Topic Id</param>
        /// <returns></returns>
        public async Task<int> AddAsyncMessageToTopic(Message entity, int id)
        {
            var topic = _context.Topics.First(x => x.Id == id);
            entity.Topic = topic;
            _context.Messages.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;

        }

        public void Delete(Message entity)
        {
            _context.Messages.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            _context.Messages.Remove(entity);


        }
        public void DeleteAllUserMessages(string userId)
        {
            var userMessages = _context.Messages.Where(x => x.ForumUser.Id == userId);
            _context.Messages.RemoveRange(userMessages);
            _context.SaveChanges();
        }

        public IQueryable<Message> FindAll()
        {
            _context.Messages.Include("ForumUser");
            _context.Messages.Include(x => x.Topic);
            return _context.Messages;
        }

        public async Task<Message> GetByIdAsync(int id)
        {

            return await _context.Messages.SingleAsync(x => x.Id == id);

        }

        public async Task Update(Message entity)
        {
            var foundEntity =  await GetByIdAsync(entity.Id);
            foundEntity.Text = entity.Text;
            _context.SaveChanges();
        }
    }
}
