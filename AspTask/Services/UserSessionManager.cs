using Microsoft.AspNetCore.Http;

namespace AspTask.Services
{
    /// <summary>
    /// Manages user session operations such as login, logout, 
    /// checking if a user is logged in, and retrieving session data.
    /// </summary>
    public static class UserSessionManager
    {
        #region Constants
        private const string LOGGED_IN_KEY = "logged-in-user-id";
        private const string USERNAME_KEY = "logged-in-user-username";
        #endregion

        #region Fields
        private static IHttpContextAccessor httpContextAccessor;
        #endregion

        #region Initialize
        /// <summary>
        /// Initializes the <see cref="UserSessionManager"/> with the provided 
        /// <see cref="IHttpContextAccessor"/> instance for session management.
        /// </summary>
        /// <param name="accessor">The accessor to retrieve HTTP context and manage sessions.</param>
        public static void Initialize(IHttpContextAccessor accessor)
        {
            httpContextAccessor = accessor;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Logs the user in by storing their user ID and username in the session.
        /// </summary>
        /// <param name="userId">The ID of the user to log in.</param>
        /// <param name="username">The username of the user to log in.</param>
        public static void Login(int userId, string username)
        {
            httpContextAccessor.HttpContext.Session.SetString(LOGGED_IN_KEY, userId.ToString());
            httpContextAccessor.HttpContext.Session.SetString(USERNAME_KEY, username);
        }

        /// <summary>
        /// Checks whether a user is currently logged in by verifying the session data.
        /// </summary>
        /// <returns>True if the user is logged in, otherwise false.</returns>
        public static bool IsUserLoggedIn()
        {
            string userId = httpContextAccessor.HttpContext.Session.GetString(LOGGED_IN_KEY);
            return !string.IsNullOrEmpty(userId);
        }

        /// <summary>
        /// Logs the user out by removing their session data.
        /// </summary>
        public static void Logout()
        {
            httpContextAccessor.HttpContext.Session.Remove(LOGGED_IN_KEY);
            httpContextAccessor.HttpContext.Session.Remove(USERNAME_KEY);
        }

        /// <summary>
        /// Retrieves the username of the currently logged-in user from the session.
        /// </summary>
        /// <returns>The username of the logged-in user, or null if not logged in.</returns>
        public static string GetUsername()
        {
            return httpContextAccessor.HttpContext.Session.GetString(USERNAME_KEY);
        }

        /// <summary>
        /// Retrieves the user ID of the currently logged-in user from the session.
        /// </summary>
        /// <returns>The user ID of the logged-in user, or null if not logged in.</returns>
        public static string CurrentUserId()
        {
            return httpContextAccessor.HttpContext.Session.GetString(LOGGED_IN_KEY);
        }
        #endregion
    }// end class
}// end namespace
