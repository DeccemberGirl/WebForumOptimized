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
        private UserService _userService;
        private IAuthenticationManager _authenticationManager;
        private UserRoleService _roleService;
        public AdminController(UserService userService, IAuthenticationManager authenticationManager, UserRoleService roleService)
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
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> AddRemoveAdmin(string id)
        {
            await _roleService.ToggleAdmin(id);
            return RedirectToAction("AdminPanel");
        }
        /// <summary>
        /// Delets User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> DeleteUser(string id)
        {
            await _userService.RemoveUser(id);
            return RedirectToAction("AdminPanel");
        }
    }
}