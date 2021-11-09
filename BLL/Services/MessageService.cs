using AutoMapper;
using BLL.Builders.Interfaces;
using BLL.DTO;
using BLL.Models;
using BLL.Services.Interfaces;
using DAL;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    /// <summary>
    /// Service for Messages
    /// </summary>
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMessageDTOBuilder _messageDTOBuilder;
        public MessageService(IUnitOfWork db, IMapper mapper, IMessageDTOBuilder messageDTOBuilder)
        {
            _mapper = mapper;
            _unitOfWork = db;
            _messageDTOBuilder = messageDTOBuilder;
        }
        /// <summary>
        /// Allows to get all messages
        /// </summary>
        public IEnumerable<MessageDTO> GetAll()
        {
            var entities = _unitOfWork.MessageRepository.FindAll();
            var result = _mapper.MapList<Message, MessageDTO>(entities);
            return result;
        }
        /// <summary>
        /// Allows to get all messages on page
        /// </summary>
        public PagedMessagesModel GetPagedMessages(IEnumerable<MessageDTO> messages, int pageNumber)
        {
            var numberOfMessages = messages.Count();
            var numberOfPages = (int)Math.Ceiling((double)numberOfMessages / (double)Constants.PageSize);
            if (pageNumber > numberOfPages)
            {
                pageNumber = 1;
            }
            var totalPages = numberOfPages;
            var messagesResult = messages.OrderBy(x => x.Id).Skip((pageNumber - 1) * Constants.PageSize).Take(Constants.PageSize);
            var pageModel = new PagedMessagesModel() { CurrentPage = pageNumber, Messages = messagesResult, TotalPages = totalPages };
            return pageModel;
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
            if (String.IsNullOrEmpty(userId) || String.IsNullOrEmpty(userName) || String.IsNullOrEmpty(text) || topicId == 0)
                return;
            var inputMessageDTO = _messageDTOBuilder.Create(userId, userName, text);
            var message = _mapper.Map<MessageDTO, Message>(inputMessageDTO);
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
        public async Task UpdateAsync(MessageDTO message)
        {
            if (message == null)
                return;
            await _unitOfWork.MessageRepository.Update(_mapper.Map<MessageDTO, Message>(message));
            await _unitOfWork.SaveAsync();
        }
    }
}
