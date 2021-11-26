using BLL.DTO;
using BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    /// <summary>
    /// Service to handle user entities
    /// </summary>
    public interface IUserService : IDisposable
    {
        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="userDto">User Dto</param>
        /// <returns>OperationDetails object</returns>
        Task<OperationDetails> Create(UserDTO userDto);

        /// <summary>
        /// Validates the user 
        /// </summary>
        /// <param name="userDto"User Dto></param>
        /// <returns>ClaimsIdentity object</returns>
        Task<ClaimsIdentity> Authenticate(UserDTO userDto);

        /// <summary>
        /// Sets initial data about users
        /// </summary>
        /// <param name="adminDto">Admin Dto</param>
        /// <param name="roles">Available roles at the forum</param>
        Task SetInitialData(UserDTO adminDto, List<string> roles);

        /// <summary>
        /// Returns the collection of all users from db
        /// </summary>
        /// <returns>The collection of users dto-s</returns>
        IEnumerable<UserDTO> GetAllUsers();

        /// <summary>
        /// Removes the user from db
        /// </summary>
        /// <param name="id">Id of the user</param>
        /// <returns>OperationDetails object</returns>
        Task<OperationDetails> RemoveUser(string id);
    }
}
