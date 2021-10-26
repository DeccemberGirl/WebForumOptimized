using DAL;
using DAL.Entities;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using NUnit.Framework;
using System.Linq;

namespace DALTests
{
    public class MessageRepositoryTests
    {
        private int _expectedMessageId;
        private int _topicId;
        private Message _message;
        private IMessageRepository _messageRepository;

        [SetUp]
        public void Setup()
        {
            _topicId = 1;
            _message = new Message() { Id = 2 };
            var forumContext = new ForumContext();
            _messageRepository = new MessageRepository(forumContext);

            var lastId = forumContext.Messages.Select(x => x.Id).Max();
            _expectedMessageId = lastId + 1;
        }

        [Test]
        public void AddAsync_AddsMessageToContext()
        {
            //Act
            var actualId = _messageRepository.AddAsync(_message).Result;
            
            //Assert
            Assert.AreEqual(_expectedMessageId, actualId);
        }

        [Test]
        public void AddAsyncMessageToTopic_AddsMessageToTopicById()
        {
            //Act
            var actualId = _messageRepository.AddAsyncMessageToTopic(_message, _topicId).Result;

            //Assert
            Assert.AreEqual(_expectedMessageId, actualId);
        }

        [TearDown]
        public void CleanUp()
        {
            _messageRepository.Delete(_message);
        }
    }
}