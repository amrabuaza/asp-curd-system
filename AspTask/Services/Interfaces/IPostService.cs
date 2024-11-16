using System.Collections.Generic;
using AspTask.Models;

namespace AspTask.Services.Interfaces
{
    /// <summary>
    /// Provides business logic and operations for managing posts, 
    /// including creation, updating, retrieval, and deletion. 
    /// This interface defines the operations that should be implemented
    /// for interacting with posts at the business logic level.
    /// </summary>
    public interface IPostService
    {
        #region Methods
        /// <summary>
        /// Creates a new post.
        /// </summary>
        /// <param name="post">The post to be created.</param>
        void CreatePost(Post post);

        /// <summary>
        /// Updates an existing post.
        /// </summary>
        /// <param name="post">The post with updated information.</param>
        void UpdatePost(Post post);

        /// <summary>
        /// Deletes a post by its ID.
        /// </summary>
        /// <param name="postId">The ID of the post to be deleted.</param>
        void DeletePost(int postId);

        /// <summary>
        /// Retrieves a post by its ID.
        /// </summary>
        /// <param name="postId">The ID of the post to be retrieved.</param>
        /// <returns>The post with the specified ID, or null if not found.</returns>
        Post GetPost(int postId);

        /// <summary>
        /// Retrieves all active posts.
        /// </summary>
        /// <returns>A collection of active posts.</returns>
        IEnumerable<Post> GetActivePosts();

        /// <summary>
        /// Retrieves all posts by a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user whose posts are to be retrieved.</param>
        /// <returns>A collection of posts created by the specified user.</returns>
        IEnumerable<Post> GetPostsByUser(string userId);
        #endregion
    } // end interface
}
