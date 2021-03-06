using BLL.DTO;
using BLL.Infrastructure;
using BLL.Services;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebForum.Models;

namespace WebForum
{
    /// <summary>
    /// Controller for all Account actions
    /// </summary>
    public class AccountController : Controller
    {
        private readonly UserService _userService;
        private readonly IAuthenticationManager _authenticationManager;

        /// <summary>
        /// Creates an instance of an <see cref="AccountController">class</see>
        /// </summary>
        /// <param name="userService">Service which handles operations with user entities</param>
        /// <param name="authenticationManager">Manager for authentication users</param>
        public AccountController(UserService userService, IAuthenticationManager authenticationManager)
        {
            _userService = userService;
            _authenticationManager = authenticationManager;
        }

        /// <summary>
        /// Shows Login page
        /// </summary>
        /// <returns>Login view</returns>
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        /// <summary>
        /// Performs Login action
        /// </summary>
        /// <param name="model">Login model</param>
        /// <returns> login model view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            await SetInitialDataAsync();
            
            if (ModelState.IsValid)
            {
                UserDTO userctrl = new UserDTO { Email = model.Email, Password = model.Password };
                ClaimsIdentity claim = await _userService.Authenticate(userctrl);
                if (claim == null)
                {
                    ModelState.AddModelError("", "Wrong Login or password.");
                }
                else
                {
                    _authenticationManager.SignOut();
                    _authenticationManager.SignIn(new AuthenticationProperties{IsPersistent = true}, claim);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        /// <summary>
        /// Performs logout action
        /// </summary>
        /// <returns>Returns Home\Index view</returns>
        public ActionResult Logout()
        {
            _authenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Reidrects to Register Form view
        /// </summary>
        /// <returns>Register form view</returns>
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Performs registration action
        /// </summary>
        /// <param name="model">Register model object</param>
        /// <returns>Registration view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            await SetInitialDataAsync();

            if (ModelState.IsValid)
            {
                UserDTO userctrl = new UserDTO
                {
                    Email = model.Email,
                    Password = model.Password,
                    Address = model.Address,
                    Name = model.Name,
                    Role = new List<string> { "user" }
                };

                OperationDetails operationDetails = await _userService.Create(userctrl);
                if (operationDetails.Succedeed)
                    return View("SuccessRegister");
                else
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(model);
        }

        /// <summary>
        /// Sets Admins user
        /// </summary>
        private async Task SetInitialDataAsync()
        {
            await _userService.SetInitialData(new UserDTO
            {
                Email = "admin@mail.ru",
                UserName = "admin@mail.ru",
                Password = "qwerty",
                Name = "Admin Adminovich",
                Address = "Administation street",
                Role = new List<string> { "admin" },
            }, new List<string> { "user", "admin" });
        }
    }
}