using System.Collections.Generic;
using AspTask.Models;
using AspTask.Repositories.Interfaces;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace AspTask.Repositories
{
    /// <summary>
    /// Provides data access operations for managing posts, including 
    /// creating, updating, retrieving, and deleting posts in the database. 
    /// Uses direct SQL queries (via <see cref="SqliteConnection"/>) to interact 
    /// with the SQLite database.
    /// </summary>
    public class PostRepository : IPostRepository
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PostRepository"/> class.
        /// </summary>
        /// <param name="configuration">The configuration used to retrieve the connection string.</param>
        public PostRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        #endregion

        #region Fields
        private readonly string connectionString;
        #endregion

        #region Methods
        /// <summary>
        /// Creates a new post and inserts it into the database.
        /// </summary>
        /// <param name="post">The post to be created.</param>
        public void CreatePost(Post post)
        {
            using SqliteConnection connection = new SqliteConnection(connectionString);
            connection.Open();
            SqliteCommand command = new SqliteCommand("INSERT INTO Posts (Title, Description, IsActive, UserId) VALUES (@Title, @Description, @IsActive, @UserId)", connection);
            command.Parameters.AddWithValue("@Title", post.Title);
            command.Parameters.AddWithValue("@Description", post.Description);
            command.Parameters.AddWithValue("@IsActive", post.IsActive);
            command.Parameters.AddWithValue("@UserId", post.UserId);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Updates an existing post in the database.
        /// </summary>
        /// <param name="post">The post with updated data.</param>
        public void UpdatePost(Post post)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = new SqliteCommand("UPDATE Posts SET Title = @Title, Description = @Description, IsActive = @IsActive WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", post.Id);
                command.Parameters.AddWithValue("@Title", post.Title);
                command.Parameters.AddWithValue("@Description", post.Description);
                command.Parameters.AddWithValue("@IsActive", post.IsActive);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Deletes a post from the database by its ID.
        /// </summary>
        /// <param name="postId">The ID of the post to be deleted.</param>
        public void DeletePost(int postId)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = new SqliteCommand("DELETE FROM Posts WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", postId);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Retrieves a post by its ID from the database.
        /// </summary>
        /// <param name="postId">The ID of the post to retrieve.</param>
        /// <returns>The post with the specified ID, or null if not found.</returns>
        public Post GetPost(int postId)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = new SqliteCommand("SELECT * FROM Posts WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", postId);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Post
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                            IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
                        };
                    }
                    return null;
                }
            }
        }

        /// <summary>
        /// Retrieves all active posts from the database.
        /// </summary>
        /// <returns>A collection of active posts.</returns>
        public IEnumerable<Post> GetActivePosts()
        {
            var posts = new List<Post>();

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand("SELECT * FROM Posts WHERE IsActive = TRUE", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        posts.Add(new Post
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                            IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
                        });
                    }
                }
            }
            return posts;
        }

        /// <summary>
        /// Retrieves posts associated with a specific user.
        /// </summary>
        /// <param name="userId">The user ID to filter posts by.</param>
        /// <returns>A collection of posts created by the specified user.</returns>
        public IEnumerable<Post> GetPostsByUser(string userId)
        {
            var posts = new List<Post>();

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                // Add a WHERE clause to filter posts by userId
                SqliteCommand command = new SqliteCommand("SELECT * FROM Posts WHERE UserId = @UserId", connection);
                command.Parameters.AddWithValue("@UserId", userId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        posts.Add(new Post
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                            IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                            UserId = userId
                        });
                    }
                }
            }
            return posts;
        }

        #endregion
    } // end class
}// end namespace
