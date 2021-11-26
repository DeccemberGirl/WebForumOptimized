using BLL.DTO;
using BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    /// <summary>
    /// Service to handle topic entities
    /// </summary>
    public interface ITopicService
    {
        /// <summary>
        /// Adds a new topic to db asynchronously
        /// </summary>
        /// <param name="topicViewformDTO">Topic DTO form with all needed information</param>
        /// <param name="userId">Id of the author</param>
        /// <param name="userName">Name of the author</param>
        Task AddAsync(TopicFormViewModel topicViewformDTO, string userId, string userName);

        /// <summary>
        /// Deletes the topic from db asynchronously
        /// </summary>
        /// <param name="topicId">Id of the topic</param>
        Task DeleteByIdAsync(int topicId);

        /// <summary>
        /// Returns the collection of all topics
        /// </summary>
        /// <returns>The collection of all topics dto-s</returns>
        IEnumerable<TopicDTO> GetAll();

        /// <summary>
        /// Returns the topic by its id
        /// </summary>
        /// <param name="id">Id of the topic</param>
        /// <param name="pageNumber">Number of the page</param>
        /// <returns>Topic dto</returns>
        Task<TopicDTO> GetById(int id, int pageNumber);

        /// <summary>
        /// Updates the topic in the db asynchronously
        /// </summary>
        /// <param name="topic">Topic dto</param>
        Task UpdateAsync(TopicDTO topic);

        /// <summary>
        /// Returns the topics from the specified page
        /// </summary>
        /// <param name="pageNumber">Number of the page</param>
        /// <returns>Model of the page with topics</returns>
        PagedTopicModel GetPagedTopics(int pageNumber);
    }
}