using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebForum
{
    /// <summary>
    /// User service
    /// </summary>
    public class UserService
    {
        private readonly UnitOfWork _database;
        private readonly IMapper _mapper;
        public UserService(UnitOfWork uow, IMapper mapper)
        {
            _database = uow;
            _mapper = mapper;
        }
        /// <summary>
        /// Registers new user
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        public async Task<OperationDetails> Create(UserGeneral userDto)
        {
            ForumUser user = await _database.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                if(String.IsNullOrEmpty(userDto.Email))
                {
                    return new OperationDetails(false, "Email is empty", "Email");
                }
                user = new ForumUser { Email = userDto.Email, UserName = userDto.Email };
                var result = await _database.UserManager.CreateAsync(user, userDto.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                
                await _database.UserManager.AddToRoleAsync(user.Id, userDto.Role.First());
                
                ForumUser clientProfile = new ForumUser { Id = user.Id, Address = userDto.Address, Name = userDto.Name };
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
        /// Deletes User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<OperationDetails> RemoveUser(string id)
        {
            ForumUser user = await _database.UserManager.FindByIdAsync(id);
            if(user==null)
            {
                return new OperationDetails(false, "There is no user with a such Id", "User");
            }
            else
            {
                if (user != null)
                {
                    await _database.ClientManager.Delete(user);
                }
                _database.MessageRepository.DeleteAllUserMessages(user.Id);
                _database.TopicRepository.DeleteAllUserTopics(user.Id);
                await _database.UserManager.DeleteAsync(user);
                return new OperationDetails(true, "User has been deleted", "");
            }
        }
        
        public async Task<ClaimsIdentity> Authenticate(UserGeneral userDto)
        {
            ClaimsIdentity claim = null;
            
            ForumUser user = await _database.UserManager.FindAsync(userDto.Email, userDto.Password);
            
            if (user != null)
                claim = await _database.UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

       
        public async Task SetInitialData(UserGeneral adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await _database.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new IdentityRole { Name = roleName };
                    await _database.RoleManager.CreateAsync(role);
                }
            }
            await Create(adminDto);
        }
        public IEnumerable<UserGeneral> GetAllUsers()
        {
            
            var users = _database.UserManager.Users;
            var outputlist = _mapper.MapList<ForumUser, UserGeneral>(users);
            foreach(var user in outputlist)
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
