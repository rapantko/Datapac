using DatapacLibrary.Data;
using DatapacLibrary.Interfaces;
using DatapacLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Diagnostics.CodeAnalysis;

namespace DatapacLibrary.Services
{
    public class NotificationRepository : IArticleRepository<Notification>
    {
        #region init
        private readonly LibraryContext context;

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="context"></param>
        public NotificationRepository([DisallowNull] LibraryContext context)
        {
            this.context = context;
        }
        #endregion

        /// <summary>
        /// Create new notification
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidDataException"></exception>
        public async Task<Notification> Create([DisallowNull] Notification item)
        {
            _ = item ?? throw new ArgumentNullException(nameof(item));

            var notification = context.Notifications.Add(item);
            await context.SaveChangesAsync();

            if (!notification.IsKeySet) { throw new InvalidDataException("Unknown error has occured"); }

            return notification.Entity;

        }

        /// <summary>
        /// Get checkout reference
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Notification?> GetById(int id)
        {
            var notification = await context.Notifications.FindAsync(id) ?? throw new InvalidDataException("Notification not found");
            return notification;
        }

        /// <summary>
        /// Delete notification
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task Remove([DisallowNull] Notification item)
        {
            _ = item ?? throw new ArgumentNullException(nameof(item));

            context.Notifications.Remove(item);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Update notification
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidDataException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<Notification> Update([DisallowNull] Notification item)
        {
            _ = item ?? throw new ArgumentNullException(nameof(item));

            EntityEntry<Notification> notification;

            try
            {
                notification = context.Notifications.Update(item);
                await context.SaveChangesAsync();
            }
            catch
            {
                throw new InvalidOperationException("Notification not found. Check provided identifier.");
            }


            if (!notification.IsKeySet) { throw new InvalidDataException("Unknown error has occured"); }

            return notification.Entity;
        }

        /// <summary>
        /// Get all
        /// </summary>
        /// <returns></returns>
        public DbSet<Notification> GetAll()
        {
            return context.Notifications;
        }
    }
}
