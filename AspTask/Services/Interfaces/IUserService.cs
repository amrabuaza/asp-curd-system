using AspTask.Models;

namespace AspTask.Services.Interfaces
{
    /// <summary>
    /// Provides business logic and operations for managing users, 
    /// including creation, updating, retrieval, and deletion.
    /// This interface defines the operations that should be implemented
    /// for interacting with users at the business logic level.
    /// </summary>
    public interface IUserService
    {
        #region Methods
        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="user">The user to be created.</param>
        /// <returns>A tuple containing a message and the created user.</returns>
        public (string message, User user) CreateUser(User user);

        /// <summary>
        /// Fetches a user by their username.
        /// </summary>
        /// <param name="username">The username of the user to retrieve.</param>
        /// <returns>The user with the specified username, or null if not found.</returns>
        public User FetchUserByUsername(string username);
        #endregion
    }// end interface
}
