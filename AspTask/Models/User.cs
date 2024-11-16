using System;
using System.ComponentModel.DataAnnotations;

namespace AspTask.Models
{
    /// <summary>
    /// Represents a user in the system with authentication details such as username and password.
    /// </summary>
    public class User
    {
        #region Constructors
        /// <summary>
        /// Creates a new instance of the <see cref="User"/> class.
        /// </summary>
        public User()
        {

        }
        #endregion

        #region Fields
        private int id;
        private string username;
        private string password;
        private string passwordHash;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The unique identifier for the user.</value>
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
        /// Gets or sets the username of the user.
        /// </summary>
        /// <value>The username of the user. This property is required and cannot exceed 50 characters.</value>
        [Required]
        [MaxLength(50)]
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
            }
        }

        /// <summary>
        /// Gets or sets the plain password for the user.
        /// </summary>
        /// <value>The password for the user. This property is required.</value>
        [Required]
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }

        /// <summary>
        /// Gets or sets the hashed password for the user.
        /// </summary>
        /// <value>The hashed password used for authentication.</value>
        public string PasswordHash
        {
            get
            {
                return passwordHash;
            }
            set
            {
                passwordHash = value;
            }
        }
        #endregion
    } // end class
} // end namespace
