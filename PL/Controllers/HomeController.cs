using BLL.DTO;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace WebForum
{
    public class HomeController : Controller
    {
        private TopicService _topicService;
        private MessageService _messageService;
        public HomeController(TopicService topicService, MessageService messageService)
        {
            _topicService = topicService;
            _messageService = messageService;
        }
        public ActionResult Index()
        {
            var allTopic = _topicService.GetAll().OrderByDescending(x=>x.Id).Take(3);
            var allMessages = _messageService.GetAll().OrderByDescending(x => x.Id).Take(3);
            var tuple = new Tuple<IEnumerable<TopicDTO>, IEnumerable<MessageDTO>>(allTopic, allMessages);
            return View(tuple);
        }

       
    }
}