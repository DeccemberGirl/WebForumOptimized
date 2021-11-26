using BLL.Builders.Interfaces;
using BLL.DTO;
using System;

namespace BLL.Builders
{
    /// <summary>
    /// Builder which transfers message information from DAL to BLL in the form of DTO
    /// </summary>
    public class MessageDTOBuilder : IMessageDTOBuilder
    {
        /// <summary>
        /// Creates Message DTO to transfer information to other classes
        /// </summary>
        /// <param name="userId">Id of the author of the message</param>
        /// <param name="userName">Name of the message author</param>
        /// <param name="text">Text of the message</param>
        /// <returns>Created messageDTO</returns>
        public MessageDTO Create(string userId, string userName, string text)
        {
            return new MessageDTO { 
                Date = DateTime.Now.ToString(), 
                UserForumId = userId, 
                UserName = userName, 
                Text = text };
        }
    }
}
