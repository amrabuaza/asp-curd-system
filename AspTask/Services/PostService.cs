using System.Collections.Generic;
using AspTask.Models;
using AspTask.Repositories.Interfaces;
using AspTask.Services.Interfaces;

namespace AspTask.Services
{
    /// <summary>
    /// Provides business logic and operations for managing posts, 
    /// including creation, updating, retrieval, and deletion. 
    /// Utilizes the <see cref="IPostRepository"/> to interact with data storage.
    /// </summary>
    public class PostService : IPostService
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PostService"/> class.
        /// </summary>
        /// <param name="postRepository">The repository used to interact with post data.</param>
        public PostService(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }
        #endregion

        #region Fields
        private readonly IPostRepository postRepository;
        #endregion

        #region Methods
        /// <summary>
        /// Creates a new post and stores it in the database.
        /// The current user's ID is automatically assigned to the post.
        /// </summary>
        /// <param name="post">The post to be created.</param>
        public void CreatePost(Post post)
        {
            // Assign current user ID to the post before creating
            post.UserId = UserSessionManager.CurrentUserId();
            postRepository.CreatePost(post);
        }

        /// <summary>
        /// Deletes an existing post.
        /// </summary>
        /// <param name="postId">The ID of the post to be deleted.</param>
        public void DeletePost(int postId)
        {
            postRepository.DeletePost(postId);
        }

        /// <summary>
        /// Retrieves all active posts.
        /// </summary>
        /// <returns>An IEnumerable of active posts.</returns>
        public IEnumerable<Post> GetActivePosts()
        {
            return postRepository.GetActivePosts();
        }

        /// <summary>
        /// Retrieves a specific post by its ID.
        /// </summary>
        /// <param name="postId">The ID of the post to retrieve.</param>
        /// <returns>The post with the specified ID.</returns>
        public Post GetPost(int postId)
        {
            return postRepository.GetPost(postId);
        }

        /// <summary>
        /// Updates an existing post in the database.
        /// </summary>
        /// <param name="post">The post with updated information.</param>
        public void UpdatePost(Post post)
        {
            postRepository.UpdatePost(post);
        }

        /// <summary>
        /// Retrieves all posts associated with a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user whose posts to retrieve.</param>
        /// <returns>An IEnumerable of posts belonging to the specified user.</returns>
        public IEnumerable<Post> GetPostsByUser(string userId)
        {
            return postRepository.GetPostsByUser(userId);
        }
        #endregion
    }// end class
}// end namespace
