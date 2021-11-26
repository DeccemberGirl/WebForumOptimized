using BLL.Builders.Interfaces;
using BLL.DTO;
using BLL.Models;
using System;

namespace BLL.Builders
{
    /// <summary>
    /// Builder which transfers topic information from DAL to BLL in the form of DTO
    /// </summary>
    public class TopicBuilder : ITopicBuilder
    {
        /// <summary>
        /// Creates Topic DTO to transfer information to other classes
        /// </summary>
        /// <param name="id">Id of the topic</param>
        /// <param name="userName">Name of the topic`s author</param>
        /// <param name="name">Topic name</param>
        /// <param name="text">Text of the topic</param>
        /// <returns>Created topicDTO</returns>
        public TopicDTO CreateTopicDTO(string id, string userName, string name, string text)
        {
            return new TopicDTO { 
                Date = DateTime.Now.ToString(), 
                UserId = id, 
                UserName = userName, 
                Text = text, 
                Name = name, 
                Messages = new PagedMessagesModel() };
        }
    }
}
