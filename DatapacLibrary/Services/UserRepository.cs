using DatapacLibrary.Data;
using DatapacLibrary.Interfaces;
using DatapacLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Diagnostics.CodeAnalysis;

namespace DatapacLibrary.Services
{
    public class UserRepository : IArticleRepository<User>
    {
        #region init
        private readonly LibraryContext context;

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="context"></param>
        public UserRepository([DisallowNull] LibraryContext context)
        {
            this.context = context;
        }
        #endregion

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidDataException"></exception>
        public async Task<User> Create([DisallowNull] User item)
        {
            _ = item ?? throw new ArgumentNullException(nameof(item));

            var user = context.Users.Add(item);
            await context.SaveChangesAsync();

            if (!user.IsKeySet) { throw new InvalidDataException("Unknown error has occured"); }

            return user.Entity;
        }

        /// <summary>
        /// Get user info
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<User?> GetById(int id)
        {
            var user = await context.Users.FindAsync(id) ?? throw new InvalidDataException("User not found");
            return user;
        }

        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task Remove([DisallowNull] User item)
        {
            _ = item ?? throw new ArgumentNullException(nameof(item));

            context.Users.Remove(item);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Update User Info
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidDataException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<User> Update([DisallowNull] User item)
        {
            _ = item ?? throw new ArgumentNullException(nameof(item));
            EntityEntry<User> user;

            try
            {
                user = context.Users.Update(item);
                await context.SaveChangesAsync();
            }
            catch
            {
                throw new InvalidOperationException("User not found. Check provided identifier.");
            }

            await context.SaveChangesAsync();

            if (!user.IsKeySet) { throw new InvalidDataException("Unknown error has occured"); }

            return user.Entity;
        }

        /// <summary>
        /// Get all
        /// </summary>
        /// <returns></returns>
        public DbSet<User> GetAll()
        {
            return context.Users;
        }
    }
}
