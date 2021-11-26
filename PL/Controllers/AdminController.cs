using BLL.Services;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WebForum
{
    /// <summary>
    /// Controller for admin panel
    /// </summary>
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly UserService _userService;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly UserRoleService _roleService;

        /// <summary>
        /// Creates an instance of an <see cref="AdminController">class</see>
        /// </summary>
        /// <param name="userService">Service which handles operations with user entities</param>
        /// <param name="authenticationManager">Manager for authentication users</param>
        /// <param name="roleService">Service to handle user roles</param>
        public AdminController(UserService userService, IAuthenticationManager authenticationManager, 
            UserRoleService roleService)
        {
            _userService = userService;
            _authenticationManager = authenticationManager;
            _roleService = roleService;
        }

        /// <summary>
        /// Shows AdminPanel Page
        /// </summary>
        /// <returns> AdminPanel view with user model</returns>
        public ActionResult AdminPanel()
        {
            var model = _userService.GetAllUsers();
            return View(model);
        }

        /// <summary>
        /// Toggles admin
        /// </summary>
        /// <param name="id">Id of the admin</param>
        /// <returns>Toggling admin view</returns>
        public async Task<ActionResult> AddRemoveAdmin(string id)
        {
            await _roleService.ToggleAdmin(id);
            return RedirectToAction("AdminPanel");
        }

        /// <summary>
        /// Delets User
        /// </summary>
        /// <param name="id">Id of the user</param>
        /// <returns>Deleting user view</returns>
        public async Task<ActionResult> DeleteUser(string id)
        {
            await _userService.RemoveUser(id);
            return RedirectToAction("AdminPanel");
        }
    }
}