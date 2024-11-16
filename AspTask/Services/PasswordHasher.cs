using System;
using System.Linq;
using System.Security.Cryptography;

namespace AspTask.Services
{
    /// <summary>
    /// Provides methods for securely hashing passwords and verifying password hashes.
    /// Uses HMACSHA256 for hashing and verification with a salt.
    /// </summary>
    public static class PasswordHasher
    {
        #region Methods

        /// <summary>
        /// Hashes a password using HMACSHA256 with a randomly generated salt.
        /// The result is a base64-encoded string containing both the salt and the hash.
        /// </summary>
        /// <param name="password">The plain-text password to be hashed.</param>
        /// <returns>
        /// A string containing the base64-encoded salt and hash, separated by a period (`.`).
        /// The format is "{salt}.{hash}".
        /// </returns>
        public static string HashPassword(string password)
        {
            using var hmac = new HMACSHA256();
            // Generate a salt using the key from HMACSHA256
            var salt = hmac.Key;
            // Compute the hash of the password using the salt
            var hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            // Return the base64-encoded salt and hash
            return $"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
        }

        /// <summary>
        /// Verifies whether a plain-text password matches the stored hashed password.
        /// </summary>
        /// <param name="password">The plain-text password to verify.</param>
        /// <param name="storedPassword">The stored password hash in the format "{salt}.{hash}".</param>
        /// <returns>True if the password is valid, otherwise false.</returns>
        public static bool VerifyPassword(string password, string storedPassword)
        {
            // Split the stored password into salt and hash
            var parts = storedPassword.Split('.');
            var salt = Convert.FromBase64String(parts[0]);
            var storedHash = Convert.FromBase64String(parts[1]);

            using var hmac = new HMACSHA256(salt);
            // Compute the hash of the entered password using the stored salt
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            // Compare the computed hash with the stored hash
            return computedHash.SequenceEqual(storedHash);
        }
        #endregion
    }// end class
}// end namespace
