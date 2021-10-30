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
    /// Service for Topics
    /// </summary>
    public class TopicService : ITopicService
    {
        private readonly ITopicBuilder _topicFactory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly INewMessageFactory _newMessageFactory;
        private readonly IMessageService _messageService;
        public TopicService(IUnitOfWork db, IMapper mapper, ITopicBuilder topicFactory, INewMessageFactory newMessageFactory
            , IMessageService messageService)
        {
            _mapper = mapper;
            _unitOfWork = db;
            _topicFactory = topicFactory;
            _newMessageFactory = newMessageFactory;
            _messageService = messageService;
        }

        public IEnumerable<TopicDTO> GetAll()
        {
            var entities = _unitOfWork.TopicRepository.FindAll();
            var result = _mapper.MapList<Topic, TopicDTO>(entities);
            return result;
        }
        public PagedTopicModel GetPagedTopics(int pageNumber)
        {
            var totalPages = 1;
            var entities = _unitOfWork.TopicRepository.GetPagedTopics(pageNumber,out totalPages).ToList();
            var result = _mapper.MapList<Topic, TopicDTO>(entities);
            var pagedModel = new PagedTopicModel() { CurrentPage = pageNumber, TotalPages = totalPages, Topics = result };
            
            return pagedModel;
        }
        public async Task<TopicDTO> GetById(int id, int pageNumber)
        {
            if(id==0)
            {
                return null;
            }
            var entity = await _unitOfWork.TopicRepository.GetByIdAsync(id);
            if(entity==null)
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
        /// Adds new topic to DB
        /// </summary>
        /// <param name="topicViewformDTO"></param>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task AddAsync(TopicFormViewModel topicViewformDTO, string userId, string userName)
        {
            if(String.IsNullOrEmpty(userId))
            {
                return;
            }
            if(String.IsNullOrEmpty(userName))
            {
                return;
            }
            if(String.IsNullOrEmpty(topicViewformDTO.Name))
            {
                return;
            }
            if(String.IsNullOrEmpty(topicViewformDTO.Text))
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
        public async Task DeleteByIdAsync(int topicId)
        {
            if(topicId==0)
            {
                return;
            }
            
            await _unitOfWork.TopicRepository.DeleteByIdAsync(topicId);
            await _unitOfWork.SaveAsync();
        }
        public async Task UpdateAsync(TopicDTO topic)
        {
            if(topic==null)
            {
                return;
            }
            await _unitOfWork.TopicRepository.Update(_mapper.Map<TopicDTO, Topic>(topic));
            await _unitOfWork.SaveAsync();
        }
    }
}
