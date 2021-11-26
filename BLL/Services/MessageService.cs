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
    /// Service to handle message entities
    /// </summary>
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMessageDTOBuilder _messageDTOBuilder;

        /// <summary>
        /// Creates an instance of the <see cref="MessageService">class</see>
        /// </summary>
        /// <param name="db">DataBase unit of work object</param>
        /// <param name="mapper">Mapper instance</param>
        /// <param name="messageDTOBuilder">Builder for message dto-s</param>
        public MessageService(IUnitOfWork db, IMapper mapper, IMessageDTOBuilder messageDTOBuilder)
        {
            _mapper = mapper;
            _unitOfWork = db;
            _messageDTOBuilder = messageDTOBuilder;
        }

        /// <summary>
        /// Returns all the messages from db
        /// </summary>
        /// <returns>The collection of message DTO-s</returns>
        public IEnumerable<MessageDTO> GetAll()
        {
            var entities = _unitOfWork.MessageRepository.FindAll();
            return _mapper.MapList<Message, MessageDTO>(entities);
        }

        /// <summary>
        /// Returns the page of the specified number with the specified messages
        /// </summary>
        /// <param name="messages">The collection of messages dto-s</param>
        /// <param name="pageNumber">The number of the page</param>
        /// <returns>Model of the page with messages</returns>
        public PagedMessagesModel GetPagedMessages(IEnumerable<MessageDTO> messages, int pageNumber)
        {
            var numberOfMessages = messages.Count();
            var numberOfPages = (int)Math.Ceiling((double)numberOfMessages / Constants.PageSize);

            if (pageNumber > numberOfPages)
            {
                pageNumber = 1;
            }

            var messagesResult = messages
                .OrderBy(x => x.Id).
                Skip((pageNumber - 1) * Constants.PageSize)
                .Take(Constants.PageSize);

            return new PagedMessagesModel() { 
                CurrentPage = pageNumber, 
                Messages = messagesResult, 
                TotalPages = numberOfPages };
        }

        /// <summary>
        /// Adds a new message to db asynchronously
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <param name="userName">Name of the author</param>
        /// <param name="text">Text of the message</param>
        /// <param name="topicId">Id of the topic</param>
        public async Task AddAsync(string userId, string userName, string text, int topicId)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(userName) 
                || string.IsNullOrEmpty(text) || topicId == 0)
                return;

            var inputMessageDTO = _messageDTOBuilder.Create(userId, userName, text);
            var message = _mapper.Map<MessageDTO, Message>(inputMessageDTO);

            message.ForumUser = await _unitOfWork.UserManager.FindByIdAsync(userId);

            await _unitOfWork.MessageRepository.AddAsyncMessageToTopic(message, topicId);
            await _unitOfWork.SaveAsync();
        }

        /// <summary>
        /// Deletes the message from db asynchronously
        /// </summary>
        /// <param name="messageId">Id of the message</param>
        public async Task DeleteByIdAsync(int messageId)
        {
            if (messageId == 0)
                return;

            await _unitOfWork.MessageRepository.DeleteByIdAsync(messageId);
            await _unitOfWork.SaveAsync();
        }

        /// <summary>
        /// Updates the message asynchronously
        /// </summary>
        /// <param name="message">Message DTO</param>
        public async Task UpdateAsync(MessageDTO message)
        {
            if (message == null)
                return;

            await _unitOfWork.MessageRepository.Update(_mapper.Map<MessageDTO, Message>(message));
            await _unitOfWork.SaveAsync();
        }
    }
}
