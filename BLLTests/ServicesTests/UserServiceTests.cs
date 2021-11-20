using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Services;
using DAL;
using DAL.Entities;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BLLTests.ServicesTests
{
    class UserServiceTests
    {
        private UserService _userService;
        private IUnitOfWork _db;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _db = new UnitOfWork();
            _mapper = Substitute.For<IMapper>();
            _userService = new UserService(_db, _mapper);

        }

        [Test]
        public void Create_RegistersNewUser()
        {
            //Arrange
            var random = DateTime.Now.Millisecond;
            UserDTO userDTO = new UserDTO()
            {
                Email = "newemail1@gmail.com" + random.ToString(),
                Password = "123456",
                Role = new List<string>() { "User" },
                Id = "42"
            };

            //Act
            OperationDetails receivedOperationDetails = _userService.Create(userDTO).Result;
            OperationDetails expectedOperationDetails = new OperationDetails(true, "New User created", "");

            //Assert
            Assert.AreEqual(expectedOperationDetails.Succedeed, receivedOperationDetails.Succedeed);
            Assert.AreEqual(expectedOperationDetails.Message, receivedOperationDetails.Message);
            Assert.AreEqual(expectedOperationDetails.Property, receivedOperationDetails.Property);
        }

        [Test]
        public void Create_AttemptToCreateAnExistingUser()
        {
            //Arrange
            var existingUser = _db.UserManager.Users.FirstOrDefault();
            UserDTO userDTO = new UserDTO(){ Email = existingUser.Email };

            //Act
            OperationDetails receivedOperationDetails = _userService.Create(userDTO).Result;
            OperationDetails expectedOperationDetails = new OperationDetails(false, "User with same login exists", "Email");

            //Assert
            Assert.AreEqual(expectedOperationDetails.Succedeed, receivedOperationDetails.Succedeed);
            Assert.AreEqual(expectedOperationDetails.Message, receivedOperationDetails.Message);
            Assert.AreEqual(expectedOperationDetails.Property, receivedOperationDetails.Property);
        }

        [Test]
        public void Create_AttemptToCreateUserWithoutEmail()
        {
            //Arrange
            UserDTO userDTO = new UserDTO() { };

            //Act
            OperationDetails receivedOperationDetails = _userService.Create(userDTO).Result;
            OperationDetails expectedOperationDetails = new OperationDetails(false, "Email is empty", "Email");

            //Assert
            Assert.AreEqual(expectedOperationDetails.Succedeed, receivedOperationDetails.Succedeed);
            Assert.AreEqual(expectedOperationDetails.Message, receivedOperationDetails.Message);
            Assert.AreEqual(expectedOperationDetails.Property, receivedOperationDetails.Property);
        }

        [Test]
        public void Create_AttemptToCreateUserWithIncorectCredentials_TooShortPassword()
        {
            //Arrange
            UserDTO userDTO = new UserDTO() { Email = "newemail@gmail.com", Password = "123" };

            //Act
            OperationDetails receivedOperationDetails = _userService.Create(userDTO).Result;
            OperationDetails expectedOperationDetails = new OperationDetails(false, "Passwords must be at least 6 characters.", "");

            //Assert
            Assert.AreEqual(expectedOperationDetails.Succedeed, receivedOperationDetails.Succedeed);
            Assert.AreEqual(expectedOperationDetails.Message, receivedOperationDetails.Message);
            Assert.AreEqual(expectedOperationDetails.Property, receivedOperationDetails.Property);
        }

        [Test]
        public async Task RemoveUser_DeletesUser()
        {
            //Arrange
            var random = DateTime.Now.Millisecond;
            UserDTO userDTO = new UserDTO()
            {
                Email = "newemail1@gmail.com" + random.ToString(),
                Password = "123456",
                Role = new List<string>() { "User" }
            };
            await _userService.Create(userDTO);
            ForumUser user = await _db.UserManager.FindByEmailAsync(userDTO.Email);

            //Act
            OperationDetails receivedOperationDetails = _userService.RemoveUser(user.Id).Result;
            OperationDetails expectedOperationDetails = new OperationDetails(true, "User has been deleted", "");

            //Assert
            Assert.AreEqual(expectedOperationDetails.Succedeed, receivedOperationDetails.Succedeed);
            Assert.AreEqual(expectedOperationDetails.Message, receivedOperationDetails.Message);
            Assert.AreEqual(expectedOperationDetails.Property, receivedOperationDetails.Property);
        }

        [Test]
        public void RemoveUser_AttemptToDeleteNonExistentUser()
        {
            //Act
            OperationDetails receivedOperationDetails = _userService.RemoveUser("50").Result;
            OperationDetails expectedOperationDetails = new OperationDetails(false, "There is no user with a such Id", "User");

            //Assert
            Assert.AreEqual(expectedOperationDetails.Succedeed, receivedOperationDetails.Succedeed);
            Assert.AreEqual(expectedOperationDetails.Message, receivedOperationDetails.Message);
            Assert.AreEqual(expectedOperationDetails.Property, receivedOperationDetails.Property);
        }

        [Test]
        public void Authenticate_AttemptToAuthenticateNonExistentUser()
        {
            //Arrange
            UserDTO userDTO = new UserDTO()
            {
                Email = "someemail1@gmail.com",
            };

            //Act
            ClaimsIdentity claim = _userService.Authenticate(userDTO).Result;

            //Assert
            Assert.IsNull(claim);
        }

        [Test]
        public async Task Authenticate_AuthenticationOfExistingUser()
        {
            //Arrange
            var random = DateTime.Now.Millisecond;
            UserDTO userDTO = new UserDTO()
            {
                Email = "newemail1@gmail.com" + random.ToString(),
                Password = "123456",
                Role = new List<string>() { "User" }
            };
            await _userService.Create(userDTO);

            //Act
            ClaimsIdentity claim = _userService.Authenticate(userDTO).Result;

            //Assert
            Assert.IsTrue(claim.IsAuthenticated);
        }

        [Test]
        public async Task SetInitialData_ReceivesNoNewRoles_CreatesUser()
        {
            //Arrange
            var random = DateTime.Now.Millisecond;
            UserDTO userDTO = new UserDTO()
            {
                Email = "newemail1@gmail.com" + random.ToString(),
                Password = "123456",
                Role = new List<string>() { "User" }
            };

            //Act
            await _userService.SetInitialData(userDTO, new List<string>());
            ForumUser user = await _db.UserManager.FindByEmailAsync(userDTO.Email);
            
            //Assert
            Assert.IsNotNull(user);
        }

        [Test]
        public async Task SetInitialData_ReceivesNewRole_CreatesNewRoleAndUser()
        {
            //Arrange
            var random = DateTime.Now.Millisecond;
            UserDTO userDTO = new UserDTO()
            {
                Email = "newemail1@gmail.com" + random.ToString(),
                Password = "123456",
                Role = new List<string>() { "User" }
            };
            string newRole = "TestRole";
            
            //Act
            await _userService.SetInitialData(userDTO, new List<string>() { newRole });
            ForumUser user = await _db.UserManager.FindByEmailAsync(userDTO.Email);
            ForumRole role = await _db.RoleManager.FindByNameAsync(newRole);
            
            //Assert
            Assert.IsNotNull(user);
            Assert.AreEqual(newRole, role.Name);
        }
    }
}
