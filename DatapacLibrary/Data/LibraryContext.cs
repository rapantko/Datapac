using DatapacLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace DatapacLibrary.Data
{
    /// <summary>
    /// EF DB context for our Library test scenario
    /// </summary>
    public class LibraryContext : DbContext
    {
        #region init
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="options"></param>
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Checkout> Checkouts { get; set; }
        #endregion

        /// <summary>
        /// EF creates tables that have names the same as the DbSet property names
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Book>().ToTable("Books");
            modelBuilder.Entity<Notification>().ToTable("Notifications");
            modelBuilder.Entity<Checkout>().ToTable("Checkouts");
        }
    }
}

