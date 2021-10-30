using BLL.Builders.Interfaces;
using BLL.DTO;
using BLL.Models;
using System;
using System.Collections.Generic;

namespace BLL.Builders
{
    /// <summary>
    /// Topic builder
    /// </summary>
    public class TopicBuilder : ITopicBuilder
    {
        /// <summary>
        /// Creates TopicDTO from entered parameters
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userName"></param>
        /// <param name="name"></param>
        /// <param name="text"></param>
        /// <returns>Created TopicDTO</returns>
        public TopicDTO CreateTopicDTO(string id, string userName, string name, string text)
        {
            var topicDto = new TopicDTO { Date = DateTime.Now.ToString(), UserId = id, UserName = userName, Text = text, Name = name, Messages = new PagedMessagesModel() };
            return topicDto;
        }
    }
}
