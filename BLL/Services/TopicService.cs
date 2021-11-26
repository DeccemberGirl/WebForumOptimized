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
    /// Service to handle topic entities
    /// </summary>
    public class TopicService : ITopicService
    {
        private readonly ITopicBuilder _topicFactory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly INewMessageFactory _newMessageFactory;
        private readonly IMessageService _messageService;

        /// <summary>
        /// Creates an instance of <see cref="TopicService">class</see>
        /// </summary>
        /// <param name="db">Database unit of work object</param>
        /// <param name="mapper">Mapper instance</param>
        /// <param name="topicFactory">Factory for creating topics</param>
        /// <param name="newMessageFactory">Factory for creating messages</param>
        /// <param name="messageService">Service to handle messages</param>
        public TopicService(IUnitOfWork db, IMapper mapper, ITopicBuilder topicFactory, 
            INewMessageFactory newMessageFactory, IMessageService messageService)
        {
            _mapper = mapper;
            _unitOfWork = db;
            _topicFactory = topicFactory;
            _newMessageFactory = newMessageFactory;
            _messageService = messageService;
        }

        /// <summary>
        /// Returns the collection of all topics
        /// </summary>
        /// <returns>The collection of all topics dto-s</returns>
        public IEnumerable<TopicDTO> GetAll()
        {
            var entities = _unitOfWork.TopicRepository.FindAll();
            return _mapper.MapList<Topic, TopicDTO>(entities);
        }

        /// <summary>
        /// Returns the topics from the specified page
        /// </summary>
        /// <param name="pageNumber">Number of the page</param>
        /// <returns>Model of the page with topics</returns>
        public PagedTopicModel GetPagedTopics(int pageNumber)
        {
            int totalPages;
            var entities = _unitOfWork.TopicRepository.GetPagedTopics(pageNumber,out totalPages).ToList();
            var result = _mapper.MapList<Topic, TopicDTO>(entities);

            return new PagedTopicModel() { CurrentPage = pageNumber, TotalPages = totalPages, Topics = result };
        }

        /// <summary>
        /// Returns the topic by its id
        /// </summary>
        /// <param name="id">Id of the topic</param>
        /// <param name="pageNumber">Number of the page</param>
        /// <returns>Topic dto</returns>
        public async Task<TopicDTO> GetById(int id, int pageNumber)
        {
            if (id == 0)
            {
                return null;
            }

            var entity = await _unitOfWork.TopicRepository.GetByIdAsync(id);

            if (entity == null)
            {
                return null;
            }

            var messages = _mapper.MapList<Message, MessageDTO>(entity.Messages);
            var pagedMessage = _messageService.GetPagedMessages(messages, pageNumber);

            var result = _mapper.Map<Topic, TopicDTO>(entity);
            result.Messages = pagedMessage;
            result.NewMessage = _newMessageFactory.Create(id);

            return result;
        }

        /// <summary>
        /// Adds a new topic to db asynchronously
        /// </summary>
        /// <param name="topicViewformDTO">Topic DTO form with all needed information</param>
        /// <param name="userId">Id of the author</param>
        /// <param name="userName">Name of the author</param>
        public async Task AddAsync(TopicFormViewModel topicViewformDTO, string userId, string userName)
        {
            if(string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(userName) 
                || string.IsNullOrEmpty(topicViewformDTO.Name) || string.IsNullOrEmpty(topicViewformDTO.Text))
            {
                return;
            }
            
            var topicDTO = _topicFactory.CreateTopicDTO(userId, userName, topicViewformDTO.Name, topicViewformDTO.Text);
            var user = _unitOfWork.UserManager.FindByIdAsync(userId).Result;
            var topic = _mapper.Map<TopicDTO, Topic>(topicDTO);
            topic.ForumUser = user;

            await _unitOfWork.TopicRepository.AddAsync(topic);
            await _unitOfWork.SaveAsync();
        }

        /// <summary>
        /// Deletes the topic from db asynchronously
        /// </summary>
        /// <param name="topicId">Id of the topic</param>
        public async Task DeleteByIdAsync(int topicId)
        {
            if (topicId == 0)
            {
                return;
            }
            
            await _unitOfWork.TopicRepository.DeleteByIdAsync(topicId);
            await _unitOfWork.SaveAsync();
        }

        /// <summary>
        /// Updates the topic in the db asynchronously
        /// </summary>
        /// <param name="topic">Topic dto</param>
        public async Task UpdateAsync(TopicDTO topic)
        {
            if (topic == null)
            {
                return;
            }

            await _unitOfWork.TopicRepository.Update(_mapper.Map<TopicDTO, Topic>(topic));
            await _unitOfWork.SaveAsync();
        }
    }
}
