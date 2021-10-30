using BLL.DTO;

namespace BLL.Builders.Interfaces
{
    public interface IMessageDTOBuilder
    {
        MessageDTO Create(string userId, string userName, string text);
    }
}