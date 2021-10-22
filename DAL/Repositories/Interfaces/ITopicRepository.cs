using DAL.Entities;
using System.Collections.Generic;

namespace DAL.Repositories.Interfaces
{
    /// <summary>
    /// Topic repository interface
    /// </summary>
    public interface ITopicRepository : IRepository<Topic>
    {
        /// <summary>
        /// Deletes topics by their author's Id
        /// </summary>
        /// <param name="userId">User Id</param>
        void DeleteAllUserTopics(string userId);

        /// <summary>
        /// Gets the topics from the page
        /// </summary>
        /// <param name="pageNumber">The number of the concrete page on forum</param>
        /// <param name="totalPages">The total number of the pages</param>
        /// <returns></returns>
        IEnumerable<Topic> GetPagedTopics(int pageNumber, out int totalPages);
    }
}
