using BLL.Infrastructure;
using System;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    /// <summary>
    /// Service to handle user roles
    /// </summary>
    public interface IUserRoleService : IDisposable
    {
        /// <summary>
        /// Toggles admin by his id
        /// </summary>
        /// <param name="id">Id of the admin</param>
        /// <returns>Operation details object</returns>
        Task<OperationDetails> ToggleAdmin(string id);
    }
}