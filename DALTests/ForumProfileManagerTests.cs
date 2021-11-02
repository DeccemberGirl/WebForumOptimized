using DAL;
using DAL.Entities;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DALTests
{
    public class ForumProfileManagerTests
    {
        private ForumContext _forumContext;
        private IForumProfileManager _forumProfileManager;
        
        [SetUp]
        public void Setup()
        {
            _forumContext = new ForumContext();
            _forumProfileManager = new ForumProfileManager(_forumContext);
        }

        [Test]
        public void Create_AddsForumProfileInstanceToDb()
        {
            //Arrange
            var random = DateTime.Now.Millisecond;
            var testUser = new ForumUser()
            {
                Email = "email@gmail.com" + random.ToString(),
                EmailConfirmed = true,
                PasswordHash = "123456qwerty" + random.ToString(),
                UserName = random.ToString() + "UserNameTest"
            };
            var forumProfile = new ForumProfile()
            {
                Id = testUser.Id,
                Name = random.ToString() + "NameTest",
                Address = "AddressTest" + random.ToString(),
                ForumUser = testUser
            };

            //Act
            _forumProfileManager.Create(forumProfile);
            var actualForumProfile = _forumContext.ForumProfiles.FirstOrDefault(x => x.Id == testUser.Id);

            //Assert
            Assert.AreEqual(forumProfile.Name, actualForumProfile.Name);
        }

        [Test]
        public async Task Delete_DeletesForumProfileInstanceFromDb()
        {
            //Arrange
            var random = DateTime.Now.Millisecond;
            var testUser = new ForumUser()
            {
                Email = "email1@gmail.com" + random.ToString(),
                EmailConfirmed = true,
                PasswordHash = "1234562qwerty" + random.ToString(),
                UserName = random.ToString() + "UserNameTest"
            };
            var forumProfile = new ForumProfile()
            {
                Id = testUser.Id,
                Name = random.ToString() + "NameTest2",
                Address = "AddressTest2" + random.ToString(),
                ForumUser = testUser
            };

            _forumProfileManager.Create(forumProfile);

            //Act
            await _forumProfileManager.Delete(forumProfile);
            var actualForumProfile = _forumContext.ForumProfiles.FirstOrDefault(x => x.Id == testUser.Id);

            //Assert
            Assert.IsNull(actualForumProfile);
        }
    }
}
