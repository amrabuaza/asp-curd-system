using AspTask.Models;
using Microsoft.EntityFrameworkCore;

namespace AspTask.Data
{
    /// <summary>
    /// Represents the application's database context, providing access to database entities.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class with specified options.
        /// </summary>
        /// <param name="options">The options to configure the database context.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> for users in the application.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> for posts in the application.
        /// </summary>
        public DbSet<Post> Posts { get; set; }

        /// <summary>
        /// Configures the schema needed for the database context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to configure the model for the database.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the primary key for the User entity.
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            // Configure the primary key for the Post entity.
            modelBuilder.Entity<Post>()
                .HasKey(u => u.Id);
        }
    }// end class
}// end namespace
