using System;
using AspTask.Models;
using AspTask.Repositories.Interfaces;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace AspTask.Repositories
{
    /// <summary>
    /// Provides data access operations for managing users, including 
    /// creating, updating, retrieving, and deleting users in the database. 
    /// Uses direct SQL queries (via <see cref="SqliteConnection"/>) to interact 
    /// with the SQLite database.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="configuration">The configuration used to retrieve the connection string.</param>
        public UserRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        #endregion

        #region Fields
        private readonly string connectionString;
        #endregion

        #region Methods
        /// <summary>
        /// Creates a new user and inserts it into the database.
        /// </summary>
        /// <param name="user">The user to be created.</param>
        /// <returns>A tuple containing a message and the created user.</returns>
        public (string message, User user) CreateUser(User user)
        {
            try
            {
                using (SqliteConnection connection = new SqliteConnection(connectionString))
                {
                    connection.Open();

                    // Check if the username already exists
                    SqliteCommand checkCommand = new SqliteCommand("SELECT COUNT(1) FROM Users WHERE Username = @Username", connection);
                    checkCommand.Parameters.AddWithValue("@Username", user.Username);

                    var result = checkCommand.ExecuteScalar();

                    if (Convert.ToInt32(result) > 0)
                    {
                        return ("Username already exists, please choose a different username.", null);
                    }

                    // Insert new user into the database
                    SqliteCommand command = new SqliteCommand("INSERT INTO Users (Username, PasswordHash) VALUES (@Username, @PasswordHash)", connection);
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);

                    // Execute and get the last inserted Id
                    var insertedId = command.ExecuteScalar();

                    // Set the user Id
                    user.Id = (int)Convert.ToInt64(insertedId);

                    return ("User created successfully.", user);
                }
            }
            catch (Exception ex)
            {
                return ($"An error occurred: {ex.Message}", null);
            }
        }

        /// <summary>
        /// Fetches a user from the database by their username.
        /// </summary>
        /// <param name="username">The username of the user to be fetched.</param>
        /// <returns>The user with the specified username, or null if not found.</returns>
        public User FetchUserByUsername(string username)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var command = new SqliteCommand("SELECT * FROM Users WHERE Username = @Username", connection);
                command.Parameters.AddWithValue("@Username", username);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Username = reader.GetString(reader.GetOrdinal("Username")),
                            PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash"))
                        };
                    }
                }
            }

            return null;
        }
        #endregion
    } // end class
}// end namespace
