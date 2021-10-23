using DAL;
using DAL.Entities;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

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
            _expectedMessageId = 1;
            _topicId = 1;
            _message = new Message() { Id = 1 };
            _messageRepository = new MessageRepository(new ForumContext());
           // _messageRepository.AddAsync(_message).Returns(1);
            _messageRepository.AddAsyncMessageToTopic(_message, _topicId).Returns(1);
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
    }
}