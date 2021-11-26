using BLL.Infrastructure;
using BLL.Services.Interfaces;
using DAL;
using DAL.Entities;
using System.Threading.Tasks;

namespace BLL.Services
{
    /// <summary>
    /// Service to handle user roles
    /// </summary>
    public class UserRoleService : IUserRoleService
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Creates an instance of <see cref="UserRoleService">class</see>
        /// </summary>
        /// <param name="unitOfWork">Unit of work with db object</param>
        public UserRoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Toggles admin by his id
        /// </summary>
        /// <param name="id">Id of the admin</param>
        /// <returns>Operation details object</returns>
        public async Task<OperationDetails> ToggleAdmin(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new OperationDetails(false, "User id is missing", "UserId");
            }

            ForumUser user = await _unitOfWork.UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return new OperationDetails(false, "User was not found", "Role");
            }

            var role = await _unitOfWork.RoleManager.FindByNameAsync("admin");
            if (await _unitOfWork.UserManager.IsInRoleAsync(user.Id, role.Name))
            {
                await _unitOfWork.UserManager.RemoveFromRoleAsync(user.Id, role.Name);
                return new OperationDetails(false, "Admin role has been removed", "Role");
            }

            await _unitOfWork.UserManager.AddToRoleAsync(user.Id, role.Name);
            return new OperationDetails(true, "Role has been added", "");
        }
       
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
