using BLL.DTO;
using BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IMessageService
    {
        Task AddAsync(string userId, string userName, string text, int topicId);
        Task DeleteByIdAsync(int messageId);
        Task UpdateAsync(MessageDTO message);
        IEnumerable<MessageDTO> GetAll();
        PagedMessagesModel GetPagedMessages(IEnumerable<MessageDTO> messages, int pageNumber);
    }
}