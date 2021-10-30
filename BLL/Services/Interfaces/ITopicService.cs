using BLL.DTO;
using BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface ITopicService
    {
        Task AddAsync(TopicFormViewModel topicViewformDTO, string userId, string userName);
        Task DeleteByIdAsync(int topicId);
        IEnumerable<TopicDTO> GetAll();
        Task<TopicDTO> GetById(int id, int pageNumber);
        Task UpdateAsync(TopicDTO topic);
        PagedTopicModel GetPagedTopics(int pageNumber);
    }
}