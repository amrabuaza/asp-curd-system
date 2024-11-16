using AspTask.Models;
using AspTask.Repositories.Interfaces;
using AspTask.Services.Interfaces;

namespace AspTask.Services
{
    /// <summary>
    /// Provides business logic and operations for managing users, 
    /// including creation, updating, retrieval, and deletion. 
    /// Utilizes the <see cref="IUserRepository"/> to interact with data storage.
    /// </summary>
    public class UserService : IUserService
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userRepository">The repository used to interact with user data.</param>
        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        #endregion

        #region Fields
        private readonly IUserRepository userRepository;
        #endregion

        #region Methods
        /// <summary>
        /// Creates a new user and stores it in the database. 
        /// The user's password is hashed before being stored.
        /// </summary>
        /// <param name="user">The user to be created.</param>
        /// <returns>A tuple containing a message and the created user instance.</returns>
        public (string message, User user) CreateUser(User user)
        {
            // Hash the user's password before storing
            user.PasswordHash = PasswordHasher.HashPassword(user.Password);
            return userRepository.CreateUser(user);
        }

        /// <summary>
        /// Retrieves a user by their username.
        /// </summary>
        /// <param name="username">The username of the user to retrieve.</param>
        /// <returns>The user with the specified username.</returns>
        public User FetchUserByUsername(string username)
        {
            return userRepository.FetchUserByUsername(username);
        }
        #endregion
    }// end class
}// end namespace
