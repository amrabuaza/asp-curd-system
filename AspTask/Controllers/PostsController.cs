using System.Collections.Generic;
using AspTask.Models;
using AspTask.Services;
using AspTask.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AspTask.Controllers
{
    /// <summary>
    /// Controller responsible for handling actions related to posts.
    /// </summary>
    public class PostsController : Controller
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PostsController"/> class with the specified post service.
        /// </summary>
        /// <param name="postService">An instance of <see cref="IPostService"/> to handle post-related operations.</param>
        public PostsController(IPostService postService)
        {
            this.postService = postService;
        }

        #endregion

        #region Fields

        /// <summary>
        /// A service for handling post-related business logic.
        /// </summary>
        private readonly IPostService postService;

        #endregion

        #region Actions

        /// <summary>
        /// Displays the list of posts created by the current user.
        /// </summary>
        /// <returns>A <see cref="IActionResult"/> representing the posts view with the user's posts.</returns>
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Post> posts = postService.GetPostsByUser(UserSessionManager.CurrentUserId());
            return View(posts);
        }

        /// <summary>
        /// Displays the post creation page.
        /// </summary>
        /// <returns>A <see cref="IActionResult"/> representing the post creation view.</returns>
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Handles the creation of a new post.
        /// </summary>
        /// <param name="post">The <see cref="Post"/> object containing post details.</param>
        /// <returns>
        /// A redirect to the index page if successful, or the creation view with error messages if failed.
        /// </returns>
        [HttpPost]
        public IActionResult Create(Post post)
        {
            if (ModelState.IsValid)
            {
                postService.CreatePost(post);
                return RedirectToAction("Index");
            }
            return View(post);
        }

        /// <summary>
        /// Handles the deletion of a post.
        /// </summary>
        /// <param name="post">The <see cref="Post"/> object containing the ID of the post to delete.</param>
        /// <returns>
        /// A redirect to the index page if successful, or the current view with error messages if failed.
        /// </returns>
        [HttpPost]
        public IActionResult Delete(Post post)
        {
            if (ModelState.IsValid)
            {
                postService.DeletePost(post.Id);
                return RedirectToAction("Index");
            }
            return View(post);
        }

        /// <summary>
        /// Retrieves a single post by its ID.
        /// </summary>
        /// <param name="id">The ID of the post to retrieve.</param>
        /// <returns>
        /// A JSON object containing the post details if found, or a 404 Not Found status if the post does not exist.
        /// </returns>
        [HttpGet]
        public IActionResult GetPost(int id)
        {
            var post = postService.GetPost(id);
            if (post == null)
            {
                return NotFound();
            }
            return Json(post);
        }

        /// <summary>
        /// Handles the editing of an existing post.
        /// </summary>
        /// <param name="post">The <see cref="Post"/> object containing the updated post details.</param>
        /// <returns>
        /// A redirect to the index page if successful, or the edit view with error messages if failed.
        /// </returns>
        [HttpPost]
        public IActionResult Edit(Post post)
        {
            if (ModelState.IsValid)
            {
                postService.UpdatePost(post);
                return RedirectToAction("Index");
            }
            return View(post);
        }

        #endregion
    }// end class
}// end namespace
