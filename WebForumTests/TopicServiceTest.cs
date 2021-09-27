using AutoMapper;
using WebForum;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace WebForumTests
{
    [TestClass]
    public class TopicServiceTest
    {
        private List<Topic> topicsEntity;
        private List<TopicControl> topicsDTO;
        

        [TestInitialize]
        public void Setup()
        {
            topicsEntity = new List<Topic> { new Topic { Id = 1, Date = "2010/10/10", Name = "Test Topic Name", Text = "Test First Message" },
             new Topic { Id = 2, Date = "2013/09/11", Name = "Second Test Topic Name", Text = "Second Test First Message"} };
            topicsDTO = new List<TopicControl> { new TopicControl { Id = 1, Date = "2010/10/10" , Name = "Test Topic Name", Text = "Test First Message" },
            new TopicControl {Id = 2, Date = "2013/09/11", Name = "Second Test Topic Name", Text = "Second Test First Message"} };
            
        }

        [TestMethod]
        public void Given_Valid_Entities_When_GetAll_Then_Return_DTOs()
        {
            //Arrange
            var mapper = new Mock<IMapper>();
            var unitOfWork = new Mock<UnitOfWork>();
            var messageService = new Mock<MessageService>();
            var topicService = new TopicService(unitOfWork.Object, mapper.Object, messageService.Object);
            unitOfWork.Setup(x => x.TopicRepository.FindAll()).Returns(topicsEntity.AsQueryable());
            foreach (var topic in topicsEntity)
            {
                mapper.Setup(x => x.Map<Topic,TopicControl>(topic)).Returns(topicsDTO.First(x=>x.Id==topic.Id));
            }
            //Act
            var allTopicsResult = topicService.GetAll();
            //Assert
            CollectionAssert.AreEquivalent(topicsDTO, allTopicsResult.ToList());
        }
        [TestMethod]
        public void Given_Null_Entities_When_GetAll_Then_Return_Null()
        {
            //Arrange
            var mapper = new Mock<IMapper>();
            var unitOfWork = new Mock<UnitOfWork>();
            var messageService = new Mock<MessageService>();
            var topicService = new TopicService(unitOfWork.Object, mapper.Object, messageService.Object);
            unitOfWork.Setup(x => x.TopicRepository.FindAll()).Returns((IQueryable<Topic>)null);
            //Act
            var allTopicsResult = topicService.GetAll();
            //Assert
            Assert.IsNull(allTopicsResult);
        }


        [TestMethod]
        public void Given_Valid_Entities_When_GetByIdAsync_Then_Return_DTO()
        {
            //Arrange
            var mapper = new Mock<IMapper>();
            var unitOfWork = new Mock<UnitOfWork>();
            var messageService = new Mock<MessageService>();
            var topicService = new TopicService(unitOfWork.Object, mapper.Object, messageService.Object);
            unitOfWork.Setup(x => x.TopicRepository.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(topicsEntity[1]));
            mapper.Setup(x => x.Map<Topic, TopicControl>(topicsEntity[1])).Returns(topicsDTO[1]);
         
            //Act
            var TopicResult = topicService.GetById(1,1).Result;
            //Assert
            Assert.AreEqual(topicsDTO[1], TopicResult);
        }
       
        [TestMethod]
        public void Given_Valid_Id_When_DeleteByIdAsync_Invokes_Delete_From_Repository_Once()
        {
            //Arrange
            var mapper = new Mock<IMapper>();
            var unitOfWork = new Mock<UnitOfWork>();
            var repository = new Mock<TopicRepository>();
            var messageService = new Mock<MessageService>();
            var topicService = new TopicService(unitOfWork.Object, mapper.Object, messageService.Object);
            unitOfWork.Setup(x => x.TopicRepository.DeleteByIdAsync(1)).Verifiable();
            //Act
            topicService.DeleteByIdAsync(1);
            //Assert
            unitOfWork.Verify(x => x.TopicRepository.DeleteByIdAsync(1), Times.Once);
        }
        [TestMethod]
        public void Given_Valid_Entity_When_UpdateAsync_From_Repository_Once()
        {
            //Arrange
            var mapper = new Mock<IMapper>();
            var unitOfWork = new Mock<UnitOfWork>();
            var repository = new Mock<TopicRepository>();
            var messageService = new Mock<MessageService>();
            var topicService = new TopicService(unitOfWork.Object, mapper.Object, messageService.Object);
            unitOfWork.Setup(x => x.TopicRepository.Update(topicsEntity[1])).Verifiable();
            mapper.Setup(x => x.Map<TopicControl,Topic>(topicsDTO[1])).Returns(topicsEntity[1]);
            //Act
            topicService.UpdateAsync(topicsDTO[1]).Wait();
            //Assert
            unitOfWork.Verify(x => x.TopicRepository.Update(topicsEntity[1]), Times.Once);
        }
    }
}
