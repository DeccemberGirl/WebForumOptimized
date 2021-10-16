using AutoMapper;
using WebForum;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using DataLayer.Models;

namespace WebForumTests
{
    [TestClass]
    public class MessageServiceTest
    {
        private List<Topic> topicsEntity;
        private List<TopicControl> topicsDTO;
        private List<Message> messagesEntity;
        private List<MessageControl> messagesDTO;

        [TestInitialize]
        public void Setup()
        {
            topicsEntity = new List<Topic> { new Topic { Id = 1, Date = "2010/10/10", Name = "Test Topic Name", Text = "Test First Message" },
             new Topic { Id = 2, Date = "2013/09/11", Name = "Second Test Topic Name", Text = "Second Test First Message"} };
            topicsDTO = new List<TopicControl> { new TopicControl { Id = 1, Date = "2010/10/10" , Name = "Test Topic Name", Text = "Test First Message" },
            new TopicControl {Id = 2, Date = "2013/09/11", Name = "Second Test Topic Name", Text = "Second Test First Message"} };
            messagesEntity = new List<Message> { new Message { Id = 1, Date = "2013/09/09", Text = "First message!" },
            new Message{Id = 2, Date = "2012/07/09", Text = "Second message!"} };
            messagesDTO = new List<MessageControl> { new MessageControl { Id = 1, Date = "2013/09/09", Text = "First message!", TopicId = 1 },
            new MessageControl{Id = 2, Date = "2012/07/09", Text = "Second message!", TopicId=2}};
        }
        [TestMethod]
        public void Given_Valid_Id_When_DeleteByIdAsync_Message_Invokes_Delete_From_Repository_Once()
        {
            //Arrange
            var mapper = new Mock<IMapper>();
            var unitOfWork = new Mock<UnitOfWork>();
            var repository = new Mock<MessageRepository>();
            var messageService = new MessageService(unitOfWork.Object, mapper.Object);
            unitOfWork.Setup(x => x.MessageRepository.DeleteByIdAsync(1)).Verifiable();
            //Act
            messageService.DeleteByIdAsync(1);
            //Assert
            unitOfWork.Verify(x => x.MessageRepository.DeleteByIdAsync(1), Times.Once);
        }
        [TestMethod]
        public void Given_Valid_Id_When_Update_Message_Invokes_Delete_From_Repository_Once()
        {
            //Arrange
            var mapper = new Mock<IMapper>();
            var unitOfWork = new Mock<UnitOfWork>();
            var repository = new Mock<MessageRepository>();
            var messageService = new MessageService(unitOfWork.Object, mapper.Object);
            unitOfWork.Setup(x => x.MessageRepository.Update(messagesEntity[1])).Verifiable();
            mapper.Setup(x => x.Map<MessageControl, Message>(messagesDTO[1])).Returns(messagesEntity[1]);
            //Act
            messageService.UpdateAsync(messagesDTO[1]).Wait();
            //Assert
            unitOfWork.Verify(x => x.MessageRepository.Update(messagesEntity[1]), Times.Once);
        }
    }
 }
