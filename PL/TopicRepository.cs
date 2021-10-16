
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace WebForum
{
    /// <summary>
    /// Topic repository
    /// </summary>
    public class TopicRepository 
    {
        readonly private ForumContext _context;
        public TopicRepository(ForumContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Adds topic to database
        /// </summary>
        /// <param name="entity">Topic entity</param>
        /// <returns>Added topic id</returns>
        public async Task<int> AddAsync(Topic entity)
        {
            _context.Topics.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;

        }

        public void Delete(Topic entity)
        {
            _context.Topics.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            var allTopicsById = _context.Messages.Where(x => x.Topic.Id == id);
            _context.Messages.RemoveRange(allTopicsById);
            _context.Topics.Remove(entity);
        }

        public void DeleteAllUserTopics(string userId)
        {
            var allUserTopics = _context.Topics.Where(x => x.ForumUser.Id == userId);
            var allTopicsIdsMessages = allUserTopics.SelectMany(x => x.Messages);
            _context.Messages.RemoveRange(allTopicsIdsMessages);
            _context.Topics.RemoveRange(allUserTopics);
            _context.SaveChanges();
        }
        public IQueryable<Topic> FindAll()
        {
            _context.Topics.Include("Messages");
            _context.Topics.Include("ForumUser");
            return _context.Topics;
        }

        public async Task<Topic> GetByIdAsync(int id)
        {
            _context.Topics.Include("Messages");
            return await _context.Topics.SingleAsync(x => x.Id == id);

        }
        public IEnumerable<Topic> GetPagedTopics(int pageNumber, out int totalPages)
        {
            var numberOfTopics = _context.Topics.Count();
            var numberOfPages = (int)Math.Ceiling((double)numberOfTopics / 6);
            if(pageNumber> numberOfPages)
            {
                pageNumber = 1;
            }
            totalPages = numberOfPages;
            var topics = _context.Topics.OrderBy(x=>x.Id).Skip((pageNumber - 1) * 6).Take(6);
            return topics;

        }
        public async Task Update(Topic entity)
        {
            var foundEntity = await GetByIdAsync(entity.Id);
            foundEntity.Text = entity.Text;
            foundEntity.Name = entity.Name;
            _context.SaveChanges();
        }
    }
}
