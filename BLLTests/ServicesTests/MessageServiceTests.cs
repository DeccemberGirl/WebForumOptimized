using AutoMapper;
using BLL;
using BLL.Builders.Interfaces;
using BLL.DTO;
using BLL.Models;
using BLL.Services;
using DAL;
using DAL.Entities;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLLTests.ServicesTests
{
    class MessageServiceTests
    {
        private MessageService _messageService;
        private IUnitOfWork _db;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _db = Substitute.For<IUnitOfWork>();
            _mapper = Substitute.For<IMapper>();
            var messageDTOBuilder = Substitute.For<IMessageDTOBuilder>();
            _messageService = new MessageService(_db, _mapper, messageDTOBuilder);

        }

        [Test]
        public void GetAll_DefaultMessageList_ReturnsNull()
        {
            //Act
            var actualMessages = _messageService.GetAll();

            //Assert
            Assert.IsEmpty(actualMessages);
            _mapper.Received().MapList<Message, MessageDTO>(Arg.Any<IEnumerable<Message>>());
            _db.Received().MessageRepository.FindAll();
        }

        [Test]
        public void GetPagedMessages_GetsAllMessagesFromAnExistingSpecifiedPage()
        {
            MessageDTO m1 = new MessageDTO()
            {
                Id = 40,
                UserForumId = "40",
                TopicId = 40,
                Date = "Today",
                Text = "Text0",
                UserName = "User1"
            };
            MessageDTO m2 = new MessageDTO()
            {
                Id = 41,
                UserForumId = "41",
                TopicId = 40,
                Date = "Today",
                Text = "Text1",
                UserName = "User2"
            };
            MessageDTO m3 = new MessageDTO()
            {
                Id = 42,
                UserForumId = "42",
                TopicId = 42,
                Date = "Today",
                Text = "Text2",
                UserName = "User3"
            };
            MessageDTO m4 = new MessageDTO()
            {
                Id = 43,
                UserForumId = "43",
                TopicId = 42,
                Date = "Today",
                Text = "Text3",
                UserName = "User4"
            };
            MessageDTO m5 = new MessageDTO()
            {
                Id = 44,
                UserForumId = "44",
                TopicId = 42,
                Date = "Today",
                Text = "Text4",
                UserName = "User5"
            };
            MessageDTO m6 = new MessageDTO()
            {
                Id = 45,
                UserForumId = "45",
                TopicId = 42,
                Date = "Today",
                Text = "Text5",
                UserName = "User6"
            };
            MessageDTO m7 = new MessageDTO()
            {
                Id = 46,
                UserForumId = "46",
                TopicId = 42,
                Date = "Today",
                Text = "Text6",
                UserName = "User7"
            };
            MessageDTO m8 = new MessageDTO()
            {
                Id = 47,
                UserForumId = "47",
                TopicId = 43,
                Date = "Today",
                Text = "Text7",
                UserName = "User8"
            };
            IEnumerable<MessageDTO> messages = new List<MessageDTO>(){ m1, m2, m3, m4, m5, m6, m7, m8 };

            //Act
            PagedMessagesModel receivedPagedMessageModel = _messageService.GetPagedMessages(messages, 2);
            IEnumerable<MessageDTO> receivedMessages = receivedPagedMessageModel.Messages.ToList<MessageDTO>();
            PagedMessagesModel expectedPagedMessageModel = new PagedMessagesModel()
            {
                CurrentPage = 2,
                TotalPages = 2,
                Messages = new List<MessageDTO>(){ m7, m8 }
            };

            //Assert
            Assert.AreEqual(expectedPagedMessageModel.CurrentPage, receivedPagedMessageModel.CurrentPage);
            Assert.AreEqual(expectedPagedMessageModel.TotalPages, receivedPagedMessageModel.TotalPages);
            Assert.AreEqual(expectedPagedMessageModel.Messages, receivedMessages);
        }

        [Test]
        public void GetPagedMessages_GetsAllMessagesFromNonExistentSpecifiedPage()
        {
            MessageDTO m1 = new MessageDTO()
            {
                Id = 40,
                UserForumId = "40",
                TopicId = 40,
                Date = "Today",
                Text = "Text0",
                UserName = "User1"
            };
            MessageDTO m2 = new MessageDTO()
            {
                Id = 41,
                UserForumId = "41",
                TopicId = 40,
                Date = "Today",
                Text = "Text1",
                UserName = "User2"
            };
            MessageDTO m3 = new MessageDTO()
            {
                Id = 42,
                UserForumId = "42",
                TopicId = 42,
                Date = "Today",
                Text = "Text2",
                UserName = "User3"
            };
            MessageDTO m4 = new MessageDTO()
            {
                Id = 43,
                UserForumId = "43",
                TopicId = 42,
                Date = "Today",
                Text = "Text3",
                UserName = "User4"
            };
            MessageDTO m5 = new MessageDTO()
            {
                Id = 44,
                UserForumId = "44",
                TopicId = 42,
                Date = "Today",
                Text = "Text4",
                UserName = "User5"
            };
            MessageDTO m6 = new MessageDTO()
            {
                Id = 45,
                UserForumId = "45",
                TopicId = 42,
                Date = "Today",
                Text = "Text5",
                UserName = "User6"
            };
            MessageDTO m7 = new MessageDTO()
            {
                Id = 46,
                UserForumId = "46",
                TopicId = 42,
                Date = "Today",
                Text = "Text6",
                UserName = "User7"
            };
            MessageDTO m8 = new MessageDTO()
            {
                Id = 47,
                UserForumId = "47",
                TopicId = 43,
                Date = "Today",
                Text = "Text7",
                UserName = "User8"
            };
            IEnumerable<MessageDTO> messages = new List<MessageDTO>() { m1, m2, m3, m4, m5, m6, m7, m8 };

            //Act
            PagedMessagesModel receivedPagedMessageModel = _messageService.GetPagedMessages(messages, 5);
            IEnumerable<MessageDTO> receivedMessages = receivedPagedMessageModel.Messages.ToList<MessageDTO>();
            PagedMessagesModel expectedPagedMessageModel = new PagedMessagesModel()
            {
                CurrentPage = 1,
                TotalPages = 2,
                Messages = new List<MessageDTO>() { m1, m2, m3, m4, m5, m6 }
            };

            //Assert
            Assert.AreEqual(expectedPagedMessageModel.CurrentPage, receivedPagedMessageModel.CurrentPage);
            Assert.AreEqual(expectedPagedMessageModel.TotalPages, receivedPagedMessageModel.TotalPages);
            Assert.AreEqual(expectedPagedMessageModel.Messages, receivedMessages);
        }

        [Test]
        public void DeleteByIdAsync_ChecksTheCallOfTheRequiredMethodsForDeletingMessage()
        {
            //Act
            _messageService.DeleteByIdAsync(100);

            //Assert
            _db.Received().MessageRepository.DeleteByIdAsync(Arg.Any<Int32>());
            _db.Received().SaveAsync();
        }
    }
}
