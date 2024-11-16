using AspTask.Models;
using AspTask.Services;
using AspTask.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AspTask.Controllers
{
    /// <summary>
    /// Controller responsible for user account-related actions, such as signup, login, and logout.
    /// </summary>
    public class AccountController : Controller
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class with the specified user service.
        /// </summary>
        /// <param name="userService">An instance of <see cref="IUserService"/> to handle user-related operations.</param>
        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        #endregion

        #region Fields

        /// <summary>
        /// A service for handling user-related business logic.
        /// </summary>
        private readonly IUserService userService;

        #endregion

        #region Actions

        /// <summary>
        /// Displays the signup page.
        /// </summary>
        /// <returns>A <see cref="IActionResult"/> representing the signup view.</returns>
        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        /// <summary>
        /// Handles the signup process for a new user.
        /// </summary>
        /// <param name="user">The <see cref="User"/> object containing signup details.</param>
        /// <returns>A redirect to the home page if successful, or the signup view with error messages if failed.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Signup(User user)
        {
            if (ModelState.IsValid)
            {
                (string message, User user) userData = userService.CreateUser(user);
                if (userData.user == null)
                {
                    ViewBag.errorMessage = userData.message;
                    return View();
                }
                else
                {
                    UserSessionManager.Login(userData.user.Id, user.Username);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        /// <summary>
        /// Displays the login page.
        /// </summary>
        /// <returns>A <see cref="IActionResult"/> representing the login view.</returns>
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Handles the login process for a user.
        /// </summary>
        /// <param name="user">The <see cref="User"/> object containing login details.</param>
        /// <returns>
        /// A redirect to the home page if successful, or the login view with error messages if failed.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                User userData = userService.FetchUserByUsername(user.Username);

                if (userData == null || !PasswordHasher.VerifyPassword(user.Password, userData.PasswordHash))
                {
                    ModelState.AddModelError("", "Invalid credentials");
                    return View();
                }

                UserSessionManager.Login(userData.Id, user.Username);
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        /// <summary>
        /// Logs out the current user.
        /// </summary>
        /// <returns>A redirect to the home page after logging out.</returns>
        [HttpPost]
        public IActionResult Logout()
        {
            UserSessionManager.Logout();
            return RedirectToAction("Index", "Home");
        }

        #endregion
    } // end class
}// end namespace
