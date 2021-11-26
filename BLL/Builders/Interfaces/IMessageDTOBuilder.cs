using BLL.DTO;

namespace BLL.Builders.Interfaces
{
    /// <summary>
    /// Builder which transfers message information from DAL to BLL in the form of DTO
    /// </summary>
    public interface IMessageDTOBuilder
    {
        /// <summary>
        /// Creates Message DTO to transfer information to other classes
        /// </summary>
        /// <param name="userId">Id of the author of the message</param>
        /// <param name="userName">Name of the message author</param>
        /// <param name="text">Text of the message</param>
        /// <returns>Created messageDTO</returns>
        MessageDTO Create(string userId, string userName, string text);
    }
}