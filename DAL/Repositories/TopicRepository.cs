using DAL.Entities;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    /// <summary>
    /// Topic repository
    /// </summary>
    public class TopicRepository : ITopicRepository
    {
        readonly private ForumContext _context;

        /// <summary>
        /// Creates a new instance of the <see cref="TopicRepository" />
        /// </summary>
        /// <param name="context">Forum context object</param>
        public TopicRepository(ForumContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds topic to the repository 
        /// </summary>
        /// <param name="entity">Topic object</param>
        /// <returns>Topic Id</returns>
        public async Task<int> AddAsync(Topic entity)
        {
            _context.Topics.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        /// <summary>
        /// Deletes the topic from the repository
        /// </summary>
        /// <param name="entity">Topic object</param>
        public void Delete(Topic entity)
        {
            _context.Topics.Remove(entity);
        }

        /// <summary>
        /// Deletes the topic from the repository by its id
        /// </summary>
        /// <param name="id">Topic Id</param>
        public async Task DeleteByIdAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            var allTopicsById = _context.Messages.Where(x => x.Topic.Id == id);
            _context.Messages.RemoveRange(allTopicsById);
            _context.Topics.Remove(entity);
        }

        /// <summary>
        /// Deletes topics by their author's Id
        /// </summary>
        /// <param name="userId">User Id</param>
        public void DeleteAllUserTopics(string userId)
        {
            var allUserTopics = _context.Topics.Where(x => x.ForumUser.Id == userId);
            var allTopicsIdsMessages = allUserTopics.SelectMany(x => x.Messages);
            _context.Messages.RemoveRange(allTopicsIdsMessages);
            _context.Topics.RemoveRange(allUserTopics);
            _context.SaveChanges();
        }

        /// <summary>
        /// Gets all topics of the collection from the repository
        /// </summary>
        /// <returns>Collection of topics</returns>
        public IQueryable<Topic> FindAll()
        {
            _context.Topics.Include("Messages");
            _context.Topics.Include("ForumUser");
            return _context.Topics;
        }

        /// <summary>
        /// Gets the concrete topic from the repository by its id
        /// </summary>
        /// <param name="id">Topic Id</param>
        /// <returns>Collection of topics</returns>
        public async Task<Topic> GetByIdAsync(int id)
        {
            _context.Topics.Include("Messages");
            return await _context.Topics.SingleAsync(x => x.Id == id);
        }

        /// <summary>
        /// Gets the topics from the page
        /// </summary>
        /// <param name="pageNumber">The number of the concrete page on forum</param>
        /// <param name="totalPages">The total number of the pages</param>
        /// <returns></returns>
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

        /// <summary>
        /// Updates the topic info in the repository
        /// </summary>
        /// <param name="entity">Topic object</param>
        public async Task Update(Topic entity)
        {
            var foundEntity = await GetByIdAsync(entity.Id);
            foundEntity.Text = entity.Text;
            foundEntity.Name = entity.Name;
            _context.SaveChanges();
        }
    }
}
