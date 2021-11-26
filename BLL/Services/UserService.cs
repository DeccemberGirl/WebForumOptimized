using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Services.Interfaces;
using DAL;
using DAL.Entities;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BLL.Services
{
    /// <summary>
    /// Service to handle user entities
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;

        /// <summary>
        /// Creates an instance of a <see cref="UserService">class</see>
        /// </summary>
        /// <param name="uow">Unit of work with db object</param>
        /// <param name="mapper">Mapper instance</param>
        public UserService(IUnitOfWork uow, IMapper mapper)
        {
            _database = uow;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="userDto">User Dto</param>
        /// <returns>OperationDetails object</returns>
        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            if (string.IsNullOrEmpty(userDto.Email))
            {
                return new OperationDetails(false, "Email is empty", "Email");
            }

            ForumUser user = await _database.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new ForumUser { Email = userDto.Email, UserName = userDto.Email };
                var result = await _database.UserManager.CreateAsync(user, userDto.Password);

                if (result.Errors.Count() > 0)
                {
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                }

                await _database.UserManager.AddToRoleAsync(user.Id, userDto.Role.First());

                ForumProfile clientProfile = new ForumProfile { Id = user.Id, Address = userDto.Address, Name = userDto.Name };
                _database.ClientManager.Create(clientProfile);

                await _database.SaveAsync();
                return new OperationDetails(true, "New User created", "");
            }
            else
            {
                return new OperationDetails(false, "User with same login exists", "Email");
            }
        }

        /// <summary>
        /// Removes the user from db
        /// </summary>
        /// <param name="id">Id of the user</param>
        /// <returns>OperationDetails object</returns>
        public async Task<OperationDetails> RemoveUser(string id)
        {
            ForumUser user = await _database.UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return new OperationDetails(false, "There is no user with a such Id", "User");
            }
            else
            {
                if (user.ForumProfile != null)
                {
                    await _database.ClientManager.Delete(user.ForumProfile);
                }

                _database.MessageRepository.DeleteAllUserMessages(user.Id);
                _database.TopicRepository.DeleteAllUserTopics(user.Id);

                await _database.UserManager.DeleteAsync(user);
                return new OperationDetails(true, "User has been deleted", "");
            }
        }

        /// <summary>
        /// Validates the user 
        /// </summary>
        /// <param name="userDto"User Dto></param>
        /// <returns>ClaimsIdentity object</returns>
        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ForumUser user = await _database.UserManager.FindAsync(userDto.Email, userDto.Password);
            return user != null 
                ? await _database.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie) 
                : null;
        }

        /// <summary>
        /// Sets initial data about users
        /// </summary>
        /// <param name="adminDto">Admin Dto</param>
        /// <param name="roles">Available roles at the forum</param>
        public async Task SetInitialData(UserDTO adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await _database.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ForumRole { Name = roleName };
                    await _database.RoleManager.CreateAsync(role);
                }
            }

            await Create(adminDto);
        }

        /// <summary>
        /// Returns the collection of all users from db
        /// </summary>
        /// <returns>The collection of users dto-s</returns>
        public IEnumerable<UserDTO> GetAllUsers()
        {
            var users = _database.UserManager.Users;
            var outputlist = _mapper.MapList<ForumUser, UserDTO>(users);

            foreach (var user in outputlist)
            {
                user.Role = (List<string>)_database.UserManager.GetRoles(user.Id);
            }

            return outputlist;
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}
