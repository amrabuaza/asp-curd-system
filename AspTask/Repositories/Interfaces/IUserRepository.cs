using System.Collections.Generic;
using AspTask.Models;

namespace AspTask.Repositories.Interfaces
{
    /// <summary>
    /// Defines the contract for a repository that manages <see cref="User"/> entities.
    /// </summary>
    public interface IUserRepository
    {
        #region Methods

        /// <summary>
        /// Creates a new user in the repository.
        /// </summary>
        /// <param name="user">The user to be created.</param>
        /// <returns>A tuple containing a message and the created user.</returns>
        public (string message, User user) CreateUser(User user);

        /// <summary>
        /// Retrieves a user by their username.
        /// </summary>
        /// <param name="username">The username of the user to retrieve.</param>
        /// <returns>The <see cref="User"/> with the specified username, or null if not found.</returns>
        public User FetchUserByUsername(string username);

        #endregion
    } // end interface
}// end namespace
