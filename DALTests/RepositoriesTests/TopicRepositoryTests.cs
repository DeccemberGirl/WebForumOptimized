using DAL;
using DAL.Entities;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DALTests.RepositoriesTests
{
    class TopicRepositoryTests
    {
        private Topic _topic;
        ForumContext _forumContext;
        private ITopicRepository _topicRepository;
        private int actualId;

        [SetUp]
        public void Setup()
        {
            _topic = new Topic() { Text = "Topic text", Name = "Topic name" };
            _forumContext = new ForumContext();
            _topicRepository = new TopicRepository(_forumContext);
            actualId = _topicRepository.AddAsync(_topic).Result;
        }

        [Test]
        public void AddAsync_AddsTopicToContext()
        {
            //Act
            var actualTopic = _topicRepository.GetByIdAsync(actualId).Result;

            //Assert
            Assert.AreEqual(_topic, actualTopic);
        }

        [Test]
        public void Delete_DeletesTopicFromContextUsingTopicEntity()
        {
            //Act
            _topicRepository.Delete(_topic);

            //Assert
            Assert.Throws<AggregateException>(() => { var topic = _topicRepository.GetByIdAsync(actualId).Result; });
        }

        [Test]
        public async Task DeleteByIdAsync_DeletesTopicFromContextUsingTopicId()
        {
            //Act
            await _topicRepository.DeleteByIdAsync(actualId);

            //Assert
            Assert.Throws<AggregateException>(() => { var topic = _topicRepository.GetByIdAsync(actualId).Result; });
        }

        [Test]
        public void DeleteAllUserTopics_DeletesUserTopicsByUserId()
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
            Topic topic = new Topic() { ForumUser = testUser };
            actualId = _topicRepository.AddAsync(topic).Result;

            //Act
            _topicRepository.DeleteAllUserTopics(testUser.Id);

            //Assert
            Assert.Throws<AggregateException>(() => { topic = _topicRepository.GetByIdAsync(actualId).Result; });
        }

        [Test]
        public void FindAll_FindsAllTopics()
        {
            //Act
            IQueryable<Topic> receivedRepository = _topicRepository.FindAll();
            Topic addedTopic = receivedRepository.First(x => x.Id == actualId);

            //Assert
            Assert.AreEqual(_topic, addedTopic);
        }

        [Test]
        public void GetByIdAsync_ReturnsTopicById()
        {
            //Act
            var addedTopic = _topicRepository.GetByIdAsync(actualId).Result;

            //Assert
            Assert.AreEqual(_topic, addedTopic);
        }

        [Test]
        public void GetPagedTopics_ReturnsTopicsByPageNumber()
        {
            //Arrange
            int numberOfPages = (int)Math.Ceiling((double)_forumContext.Topics.Count() / 6);
            
            //Act
            IEnumerable<Topic> topicsFromPage = _topicRepository.GetPagedTopics(numberOfPages, out numberOfPages);
            Topic topicOnPage = topicsFromPage.FirstOrDefault(x => x.Id == actualId);

            //Assert
            Assert.AreEqual(_topic, topicOnPage);
        }

        [Test]
        public async Task Update_UpdatesTopicTextAndName()
        {
            //Arrange
            _topic.Name = "test name";
            _topic.Text = "test text";

            //Act
            await _topicRepository.Update(_topic);
            string newTopicName = _topicRepository.GetByIdAsync(actualId).Result.Name;
            string newTopicText = _topicRepository.GetByIdAsync(actualId).Result.Text;

            //Assert
            Assert.AreEqual("test text", newTopicText); 
            Assert.AreEqual("test name", newTopicName);
        }

        [TearDown]
        public void CleanUp()
        {
            try
            {
                _topicRepository.Delete(_topic);
            }
            catch (InvalidOperationException e)
            {
                return;
            }
        }
    }
}
