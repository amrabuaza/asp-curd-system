using System.ComponentModel.DataAnnotations;

namespace AspTask.Models
{
    /// <summary>
    /// Represents a post created by a user with a title, description, and status.
    /// </summary>
    public class Post
    {
        #region Constructors
        /// <summary>
        /// Creates a new instance of the <see cref="Post"/> class.
        /// </summary>
        public Post()
        {

        }
        #endregion

        #region Fields
        private int id;
        private string title;
        private string description;
        private bool isActive;
        private string userId;
        private User user;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the post identifier.
        /// </summary>
        /// <value>The unique identifier for the post.</value>
        [Key]
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        /// <summary>
        /// Gets or sets the title of the post.
        /// </summary>
        /// <value>The title of the post. This property is required.</value>
        [Required]
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }

        /// <summary>
        /// Gets or sets the description of the post.
        /// </summary>
        /// <value>The description of the post. This property is required.</value>
        [Required]
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }

        /// <summary>
        /// Gets or sets the status of the post, indicating whether it is active.
        /// </summary>
        /// <value><c>true</c> if the post is active; otherwise, <c>false</c>.</value>
        public bool IsActive
        {
            get
            {
                return isActive;
            }
            set
            {
                isActive = value;
            }
        }

        /// <summary>
        /// Gets or sets the identifier of the user who created the post.
        /// </summary>
        /// <value>The user identifier associated with the post.</value>
        public string UserId
        {
            get
            {
                return userId;
            }
            set
            {
                userId = value;
            }
        }

        /// <summary>
        /// Gets or sets the user who created the post.
        /// </summary>
        /// <value>The <see cref="User"/> object representing the post's creator.</value>
        public User User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
            }
        }
        #endregion
    } // end class
} // end namespace
