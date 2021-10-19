﻿using AutoMapper;
using BackLayer;
using BackLayer.Models;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackLayer.Servises
{
    /// <summary>
    /// Service for Messages
    /// </summary>
    public class MessageService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MessageService(UnitOfWork db, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = db;
        }
        public IEnumerable<MessageControlModel> GetAll()
        {
            var entities = _unitOfWork.MessageRepository.FindAll();
            var result = _mapper.MapList<Message, MessageControlModel>(entities);
            return result;
        }
        public PagedMessagesModel GetPagedMessages(IEnumerable<MessageControlModel> messages, int pageNumber)
        {
            var numberOfMessages = messages.Count();
            var numberOfPages = (int)Math.Ceiling((double)numberOfMessages / 6);
            if (pageNumber > numberOfPages)
            {
                pageNumber = 1;
            }
            var totalPages = numberOfPages;
            var messagesResult = messages.OrderBy(x => x.Id).Skip((pageNumber - 1) * 6).Take(6);
            var pageModel = new PagedMessagesModel() { CurrentPage = pageNumber, Messages = messagesResult, TotalPages = totalPages };
            return pageModel;
        }

        public MessageControlModel Create(string userId, string userName, string text)
        {
            var messageDTO = new MessageControlModel { Date = DateTime.Now.ToString(), UserForumId = userId, UserName = userName, Text = text };
            return messageDTO;
        }

        /// <summary>
        /// Adds new message to topic
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        /// <param name="text"></param>
        /// <param name="topicId"></param>
        /// <returns></returns>
        public async Task AddAsync(string userId, string userName, string text, int topicId)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(text) || topicId == 0)
                return;
            var inputMessageDTO = Create(userId, userName, text);
            var message = _mapper.Map<MessageControlModel, Message>(inputMessageDTO);
            message.ForumUser = await _unitOfWork.UserManager.FindByIdAsync(userId);
            await _unitOfWork.MessageRepository.AddAsyncMessageToTopic(message, topicId);
            await _unitOfWork.SaveAsync();
        }
        /// <summary>
        /// Deletes message by its Id
        /// </summary>
        /// <param name="messageId">Id of the message wich needs to be deleted</param>
        /// <returns></returns>
        public async Task DeleteByIdAsync(int messageId)
        {
            if (messageId == 0)
                return;
            await _unitOfWork.MessageRepository.DeleteByIdAsync(messageId);
            await _unitOfWork.SaveAsync();
        }
        /// <summary>
        /// Updates message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task UpdateAsync(MessageControlModel message)
        {
            if (message == null)
                return;
            await _unitOfWork.MessageRepository.Update(_mapper.Map<MessageControlModel, Message>(message));
            await _unitOfWork.SaveAsync();
        }
    }
}
