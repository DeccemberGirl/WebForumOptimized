using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebForum
{
    /// <summary>
    /// Service for Topics
    /// </summary>
    public class TopicService 
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly MessageService _messageService;
        public TopicService(UnitOfWork db, IMapper mapper, MessageService messageService)
        {
            _mapper = mapper;
            _unitOfWork = db;
            _messageService = messageService;
        }

        /// <summary>
        /// Creates topicControl from entered parameters
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userName"></param>
        /// <param name="name"></param>
        /// <param name="text"></param>
        /// <returns>Created topicControl</returns>
        public TopicControl CreateTopicControl(string id, string userName, string name, string text)
        {
            var topicControl = new TopicControl { Date = DateTime.Now.ToString(), UserId = id, UserName = userName, Text = text, Name = name, Messages = new PagedMessagesModel() };
            return topicControl;
        }

        public NewMessageFormModel Create(int topicId)
        {
            var message = new NewMessageFormModel { TopicId = topicId };
            return message;
        }

        public IEnumerable<TopicControl> GetAll()
        {
            var entities = _unitOfWork.TopicRepository.FindAll();
            var result = _mapper.MapList<Topic, TopicControl>(entities);
            return result;
        }
        public PagedTopicModel GetPagedTopics(int pageNumber)
        {
            var totalPages = 1;
            var entities = _unitOfWork.TopicRepository.GetPagedTopics(pageNumber,out totalPages).ToList();
            var result = _mapper.MapList<Topic, TopicControl>(entities);
            var pagedModel = new PagedTopicModel() { CurrentPage = pageNumber, TotalPages = totalPages, Topics = result };
            
            return pagedModel;
        }
        public async Task<TopicControl> GetById(int id, int pageNumber)
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
            var messages = _mapper.MapList<Message, MessageControl>(entity.Messages);
            var pagedMessage = _messageService.GetPagedMessages(messages, pageNumber);
            var result = _mapper.Map<Topic, TopicControl>(entity);
            result.Messages = pagedMessage;
            result.NewMessage = Create(id);
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
            var topicControl = CreateTopicControl(userId, userName, topicViewformDTO.Name, topicViewformDTO.Text);
            var user = _unitOfWork.UserManager.FindByIdAsync(userId).Result;
            var topic = _mapper.Map<TopicControl, Topic>(topicControl);
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
        public async Task UpdateAsync(TopicControl topic)
        {
            if(topic==null)
            {
                return;
            }
            await _unitOfWork.TopicRepository.Update(_mapper.Map<TopicControl, Topic>(topic));
            await _unitOfWork.SaveAsync();
        }
    }
}
