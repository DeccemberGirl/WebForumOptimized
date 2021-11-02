using DAL;
using DAL.Entities;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DALTests.RepositoriesTests
{
    class MessageRepositoryTests
    {
        private int _expectedMessageId;
        private int _topicId;
        private Message _message;
        private IMessageRepository _messageRepository;
        private ForumUser _forumUser;
        private int actualId;

        [SetUp]
        public void Setup()
        {
            _topicId = 1;
            _message = new Message() { Id = 50, Text = "some text" };
            var forumContext = new ForumContext();
            _messageRepository = new MessageRepository(forumContext);
            
            actualId = _messageRepository.AddAsync(_message).Result;
        }

        [Test]
        public void AddAsync_AddsMessageToContext()
        {
            //Act
            var actualMessage = _messageRepository.GetByIdAsync(actualId).Result;

            //Assert
            Assert.AreEqual(_message, actualMessage);
        }

        [Test]
        public void AddAsyncMessageToTopic_AddsMessageToTopicById()
        {
            //Act
            var actualMessage = _messageRepository.GetByIdAsync(actualId).Result;


            //Assert
            Assert.AreEqual(_message, actualMessage);
        }

        [Test]
        public void Delete_DeletesMessageFromContextUsingMessageEntity()
        {
            //Act
            _messageRepository.Delete(_message);
            
            //Assert
            Assert.Throws<AggregateException>(() => { var mess = _messageRepository.GetByIdAsync(actualId).Result; });
        }

        [Test]
        public async Task DeleteByIdAsync_DeletesMessageFromContextUsingMessageId()
        {
            //Act
            await _messageRepository.DeleteByIdAsync(actualId);
            
            //Assert
            Assert.Throws<AggregateException>(() => { var mess = _messageRepository.GetByIdAsync(actualId).Result; });
        }

        [Test]
        public void DeleteAllUserMessages_DeletesUserMessagesByUserId()
        {
            //Arrange
            var random = DateTime.Now.Millisecond;
            var testUser = new ForumUser()
            {
                Email = "email@gmail.com" + random.ToString(),
                EmailConfirmed = true,
                PasswordHash = "1234562qwerty" + random.ToString(),
                UserName = random.ToString() + "TestUserName"
            };
            Message message = new Message() { Id = 55, Text = "some text", ForumUser=testUser };
            actualId = _messageRepository.AddAsync(message).Result;

            //Act
            _messageRepository.DeleteAllUserMessages(testUser.Id);

            //Assert
            Assert.Throws<AggregateException>(() => { var mess = _messageRepository.GetByIdAsync(actualId).Result; });
        }

        [Test]
        public void FindAll_FindsAllMessages()
        {
            //Act
            IQueryable<Message> receivedRepository = _messageRepository.FindAll();
            Message addedMessage = receivedRepository.First(x => x.Id == actualId);

            //Assert
            Assert.AreEqual(_message, addedMessage);
        }

        [Test]
        public void GetByIdAsync_ReturnsMessageById()
        {
            //Act
            var addedMessage = _messageRepository.GetByIdAsync(actualId).Result;

            //Assert
            Assert.AreEqual(_message, addedMessage);
        }

        [Test]
        public async Task Update_UpdatesMessageText()
        {
            //Act
            _message.Text = "test text";
            await _messageRepository.Update(_message);
            string newMessageText = _messageRepository.GetByIdAsync(actualId).Result.Text;

            //Assert
            Assert.AreEqual("test text", newMessageText);
        }

        [TearDown]
        public void CleanUp()
        {
            try
            {
                _messageRepository.Delete(_message);
            }
            catch (InvalidOperationException e)
            {
                return;
            }
        }
    }
}