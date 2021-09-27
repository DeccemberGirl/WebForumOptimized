using System;
using System.Threading.Tasks;

namespace WebForum
{
    /// <summary>
    /// Service for User Roles
    /// </summary>
    public class UserRoleService
    {
        private readonly UnitOfWork _unitOfWork;
        public UserRoleService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Removes or Adds Admin role to user
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns></returns>
        public async Task<OperationDetails> ToggleAdmin(string id)
        {

            if(String.IsNullOrEmpty(id))
            {
                return new OperationDetails(false, "User id is missing", "UserId");
            }
            ForumUser user = await _unitOfWork.UserManager.FindByIdAsync(id);
            if(user==null)
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
