using DatapacLibrary.Data;
using DatapacLibrary.Interfaces;
using DatapacLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Diagnostics.CodeAnalysis;

namespace DatapacLibrary.Services
{
    public class CheckoutRepository : IArticleRepository<Checkout>
    {
        #region init
        private readonly LibraryContext context;

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="context"></param>
        public CheckoutRepository([DisallowNull] LibraryContext context)
        {
            this.context = context;
        }

        #endregion
        /// <summary>
        /// Create new checkout
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidDataException"></exception>
        public async Task<Checkout> Create([DisallowNull] Checkout item)
        {
            _ = item ?? throw new ArgumentNullException(nameof(item));

            var checkout = context.Checkouts.Add(item);
            await context.SaveChangesAsync();

            if (!checkout.IsKeySet) { throw new InvalidDataException("Unknown error has occured"); }

            return checkout.Entity;

        }

        /// <summary>
        /// Get checkout reference
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Checkout?> GetById(int id)
        {
            var checkout = await context.Checkouts.FindAsync(id) ?? throw new InvalidDataException("Checkout record not found");
            return checkout;
        }

        /// <summary>
        /// Delete checkout
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task Remove([DisallowNull] Checkout item)
        {
            _ = item ?? throw new ArgumentNullException(nameof(item));

            context.Checkouts.Remove(item);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Update checkout
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidDataException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<Checkout> Update([DisallowNull] Checkout item)
        {
            _ = item ?? throw new ArgumentNullException(nameof(item));

            EntityEntry<Checkout> checkout;

            try
            {
                checkout = context.Checkouts.Update(item);
                await context.SaveChangesAsync();
            }
            catch
            {
                throw new InvalidOperationException("Checkout not found. Check provided identifier.");
            }


            if (!checkout.IsKeySet) { throw new InvalidDataException("Unknown error has occured"); }

            return checkout.Entity;
        }

        /// <summary>
        /// Get all
        /// </summary>
        /// <returns></returns>
        public DbSet<Checkout> GetAll()
        {
            return context.Checkouts;
        }
    }
}
