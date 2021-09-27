using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WebForum
{
    /// <summary>
    /// Controller for all Forum actions
    /// </summary>
    public class ForumController : Controller
    {
        private UserService _userService;
        private TopicService _topicService;
        private MessageService _messageService;
        
        public ForumController(UserService userService, TopicService topicService, MessageService messageService)
        {
            _userService = userService;
            _topicService = topicService;
            _messageService = messageService;
        }
        /// <summary>
        /// Displays All Topics
        /// </summary>
        /// <returns>View and model with all topics</returns>
        public ActionResult AllTopics(int currentPage = 1)
        {
            var topics =_topicService.GetPagedTopics(currentPage);
            return View(topics);
        }
        /// <summary>
        /// Displays choosen Topic
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> DisplayTopic(int id, int currentPage = 1)
        {
            var topic = await _topicService.GetById(id, currentPage);
            
            if(topic==null)
            {
                return new HttpStatusCodeResult(404);
            }
            return View("Topic", topic);

        }
        /// <summary>
        /// Redirects to Topic Creation Form
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult AddTopicForm()
        {
            return View();
        }
        /// <summary>
        /// Adds topic 
        /// </summary>
        /// <param name="topicControl"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddTopic(TopicFormViewModel topicControl)
        {
            var userName = User.Identity.Name;
            var userId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                if(String.IsNullOrEmpty(topicControl.Text))
                {
                    ModelState.AddModelError("", "Message is null!");
                }
                if (String.IsNullOrEmpty(topicControl.Name))
                {
                    ModelState.AddModelError("", "Topic is null!");
                }
                await _topicService.AddAsync(topicControl, userId, userName);
            }
            return RedirectToAction("AllTopics");
        }
        /// <summary>
        /// Adds new message to topic
        /// </summary>
        /// <param name="newMessage"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddNewMessage(NewMessageFormModel newMessage)
        {
           
            var userName = User.Identity.Name;
            var userId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                if (newMessage.Text==null)
                {
                    ModelState.AddModelError("", "Message should be filled");
                }
                if( newMessage.TopicId==0)
                {
                    ModelState.AddModelError("", "Empty Topic Id!");
                }
                await _messageService.AddAsync(userId, userName, newMessage.Text, newMessage.TopicId);
            }
            return RedirectToAction("DisplayTopic",new { id = newMessage.TopicId });
        }
        /// <summary>
        /// Delete message from topic. Only for admins
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="topicId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeleteMessage(int messageId, int topicId)
        {
            await _messageService.DeleteByIdAsync(messageId);
            return RedirectToAction("DisplayTopic", new { id = topicId });
        }
        /// <summary>
        /// Delete topic. Only for admins.
        /// </summary>
        /// <param name="topicId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeleteTopic(int topicId)
        {
            await _topicService.DeleteByIdAsync(topicId);
            return RedirectToAction("AllTopics");
        }
        /// <summary>
        /// Redirects to Edit Message Form if current user is a message author
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult EditMessageForm(MessageControl model)
        {
            if(User.Identity.GetUserId()!=model.UserForumId)
            {
                return new HttpStatusCodeResult(403);
            }
            return View(model);
        }
        /// <summary>
        /// Redirects to Edit Topic Form, if current user is a topic starter
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult EditTopicForm(TopicControl model)
        {
            if (User.Identity.GetUserId() != model.UserId)
            {
                return new HttpStatusCodeResult(403);
            }
            ModelState.Remove("Messages");
            return View(model);
        }
        /// <summary>
        /// Edits message
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        public async Task<ActionResult> EditMessage(MessageControl model)
        {
            var userName = User.Identity.Name;
            var userId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                if(model.Text==null)
                {
                    ModelState.AddModelError("", "Message is empty!");
                }
                if(model.TopicId==0)
                {
                    ModelState.AddModelError("", "Empty Topic Id!");
                }
                if(model.UserForumId!=userId || String.IsNullOrEmpty(model.UserForumId))
                {
                    ModelState.AddModelError("", "Invalid\\Empty UserId");
                }
                if(model.UserName!=userName||String.IsNullOrEmpty(model.UserName))
                {
                    ModelState.AddModelError("", "Invalid\\Empty User Name");
                }
                if(model.Id==0)
                {
                    ModelState.AddModelError("", "Message Id is null");
                }
                if(String.IsNullOrEmpty(model.Date))
                {
                    ModelState.AddModelError("", "Date cant be empty!");
                }
                await _messageService.UpdateAsync(model);
            }
            return RedirectToAction("DisplayTopic", new { id = model.TopicId });
        }
        /// <summary>
        /// Edits topic
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        public async Task<ActionResult> EditTopic(TopicControl model)
        {
            var userName = User.Identity.Name;
            var userId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                if (String.IsNullOrEmpty(model.Date))
                {
                    ModelState.AddModelError("", "Date cant be empty!");
                }
                if (model.UserId != userId || String.IsNullOrEmpty(model.UserId))
                {
                    ModelState.AddModelError("", "Invalid\\Empty UserId");
                }
                if (model.UserName != userName || String.IsNullOrEmpty(model.UserName))
                {
                    ModelState.AddModelError("", "Invalid\\Empty User Name");
                }
                if (model.Text == null)
                {
                    ModelState.AddModelError("", "Message should be filled");
                }
                if (model.Id == 0)
                {
                    ModelState.AddModelError("", "Empty Topic Id!");
                }
                await _topicService.UpdateAsync(model);
            }
            return RedirectToAction("DisplayTopic", new { id = model.Id });
        }
    }
}