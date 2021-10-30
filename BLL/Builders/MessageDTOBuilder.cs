using BLL.Builders.Interfaces;
using BLL.DTO;
using System;

namespace BLL.Builders
{
    /// <summary>
    /// Builder class for MessageDTO
    /// </summary>
    public class MessageDTOBuilder : IMessageDTOBuilder
    {
        /// <summary>
        /// Creates MessageDTO
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        /// <param name="text"></param>
        /// <returns>created messageDTO</returns>
        public MessageDTO Create(string userId, string userName, string text)
        {
            var messageDTO = new MessageDTO { Date = DateTime.Now.ToString(), UserForumId = userId, UserName = userName, Text = text };
            return messageDTO;
        }
    }
}
