using BLL.DTO;
using BLL.Models;
using BLL.Services;
using Microsoft.AspNet.Identity;
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

        /// <summary>
        /// Creates an instance of a <see cref="ForumController">class</see>
        /// </summary>
        /// <param name="userService">Service which handles operations with user entities</param>
        /// <param name="topicService">Service which handles operations with topic entities</param>
        /// <param name="messageService">Service which handles operations with message entities</param>
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
            var topics = _topicService.GetPagedTopics(currentPage);
            return View(topics);
        }

        /// <summary>
        /// Displays choosen Topic
        /// </summary>
        /// <param name="id">Id of the topic</param>
        /// <returns>Topic view</returns>
        public async Task<ActionResult> DisplayTopic(int id, int currentPage = 1)
        {
            var topic = await _topicService.GetById(id, currentPage);
            if (topic == null)
            {
                return new HttpStatusCodeResult(404);
            }

            return View("Topic", topic);
        }

        /// <summary>
        /// Redirects to Topic Creation Form
        /// </summary>
        /// <returns>Topic creation form view</returns>
        [Authorize]
        public ActionResult AddTopicForm()
        {
            return View();
        }

        /// <summary>
        /// Adds topic 
        /// </summary>
        /// <param name="topicControl">Topic form model</param>
        /// <returns>Adding topic form view</returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddTopic(TopicFormViewModel topicControl)
        {
            var userName = User.Identity.Name;
            var userId = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(topicControl.Text))
                {
                    ModelState.AddModelError("", "Message is null!");
                }
                if (string.IsNullOrEmpty(topicControl.Name))
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
        /// <param name="newMessage">Message creation form model</param>
        /// <returns>Message creation form view</returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddNewMessage(NewMessageFormModel newMessage)
        {
            var userName = User.Identity.Name;
            var userId = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                if (newMessage.Text == null)
                {
                    ModelState.AddModelError("", "Message should be filled");
                }
                if (newMessage.TopicId == 0)
                {
                    ModelState.AddModelError("", "Empty Topic Id!");
                }

                await _messageService.AddAsync(userId, userName, newMessage.Text, newMessage.TopicId);
            }

            return RedirectToAction("DisplayTopic", new { id = newMessage.TopicId });
        }

        /// <summary>
        /// Deletes message from topic. Only for admins
        /// </summary>
        /// <param name="messageId">Id of the message</param>
        /// <param name="topicId">Id of the topic</param>
        /// <returns>Message deletion view</returns>
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
        /// <param name="topicId">Id of the topic</param>
        /// <returns>Topic deletion view</returns>
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
        /// <param name="model">Message dto</param>
        /// <returns>Edit Message Form view</returns>
        [Authorize]
        public ActionResult EditMessageForm(MessageDTO model)
        {
            if (User.Identity.GetUserId() != model.UserForumId)
            {
                return new HttpStatusCodeResult(403);
            }

            return View(model);
        }

        /// <summary>
        /// Redirects to Edit Topic Form, if current user is a topic starter
        /// </summary>
        /// <param name="model">Topic dto</param>
        /// <returns>Edit Topic Form view</returns>
        [Authorize]
        public ActionResult EditTopicForm(TopicDTO model)
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
        /// <param name="model">Message dto</param>
        /// <returns>Edit message view</returns>
        [Authorize]
        public async Task<ActionResult> EditMessage(MessageDTO model)
        {
            var userName = User.Identity.Name;
            var userId = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                if (model.Text == null)
                {
                    ModelState.AddModelError("", "Message is empty!");
                }
                if (model.TopicId == 0)
                {
                    ModelState.AddModelError("", "Empty Topic Id!");
                }
                if (model.UserForumId != userId || string.IsNullOrEmpty(model.UserForumId))
                {
                    ModelState.AddModelError("", "Invalid\\Empty UserId");
                }
                if (model.UserName != userName || string.IsNullOrEmpty(model.UserName))
                {
                    ModelState.AddModelError("", "Invalid\\Empty User Name");
                }
                if (model.Id == 0)
                {
                    ModelState.AddModelError("", "Message Id is null");
                }
                if (string.IsNullOrEmpty(model.Date))
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
        /// <param name="model">Topic dto</param>
        /// <returns>edit topic view</returns>
        [Authorize]
        public async Task<ActionResult> EditTopic(TopicDTO model)
        {
            var userName = User.Identity.Name;
            var userId = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(model.Date))
                {
                    ModelState.AddModelError("", "Date cant be empty!");
                }
                if (model.UserId != userId || string.IsNullOrEmpty(model.UserId))
                {
                    ModelState.AddModelError("", "Invalid\\Empty UserId");
                }
                if (model.UserName != userName || string.IsNullOrEmpty(model.UserName))
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