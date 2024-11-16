using System.Collections.Generic;
using AspTask.Models;

namespace AspTask.Repositories.Interfaces
{
    /// <summary>
    /// Defines the contract for a repository that manages <see cref="Post"/> entities.
    /// </summary>
    public interface IPostRepository
    {
        #region Methods

        /// <summary>
        /// Creates a new post in the repository.
        /// </summary>
        /// <param name="post">The post to be created.</param>
        void CreatePost(Post post);

        /// <summary>
        /// Updates an existing post in the repository.
        /// </summary>
        /// <param name="post">The post with updated information.</param>
        void UpdatePost(Post post);

        /// <summary>
        /// Deletes a post from the repository.
        /// </summary>
        /// <param name="postId">The identifier of the post to be deleted.</param>
        void DeletePost(int postId);

        /// <summary>
        /// Retrieves a post by its identifier.
        /// </summary>
        /// <param name="postId">The identifier of the post to retrieve.</param>
        /// <returns>The <see cref="Post"/> with the specified identifier, or null if not found.</returns>
        Post GetPost(int postId);

        /// <summary>
        /// Retrieves all active posts from the repository.
        /// </summary>
        /// <returns>An enumerable collection of active posts.</returns>
        IEnumerable<Post> GetActivePosts();

        /// <summary>
        /// Retrieves all posts created by a specific user.
        /// </summary>
        /// <param name="userId">The identifier of the user whose posts are to be retrieved.</param>
        /// <returns>An enumerable collection of posts created by the specified user.</returns>
        IEnumerable<Post> GetPostsByUser(string userId);

        #endregion
    } // end interface
}// end namespace
