using DatapacLibrary.Data;
using DatapacLibrary.Interfaces;
using DatapacLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Diagnostics.CodeAnalysis;

namespace DatapacLibrary.Services
{
    public class BookRepository : IArticleRepository<Book>
    {
        #region init
        private readonly LibraryContext context;

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="context"></param>
        public BookRepository([DisallowNull] LibraryContext context)
        {
            this.context = context;
        }
        #endregion

        /// <summary>
        /// Create new book
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidDataException"></exception>
        public async Task<Book> Create([DisallowNull] Book item)
        {
            _ = item ?? throw new ArgumentNullException(nameof(item));

            var book = context.Books.Add(item);
            await context.SaveChangesAsync();

            if (!book.IsKeySet) { throw new InvalidDataException("Unknown error has occured"); }

            return book.Entity;

        }

        /// <summary>
        /// Get book reference
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Book?> GetById(int id)
        {
            var book = await context.Books.FindAsync(id) ?? throw new InvalidDataException("Book not found");
            return book;
        }

        /// <summary>
        /// Delete book
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task Remove([DisallowNull] Book item)
        {
            _ = item ?? throw new ArgumentNullException(nameof(item));

            context.Books.Remove(item);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Update book
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidDataException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<Book> Update([DisallowNull] Book item)
        {
            _ = item ?? throw new ArgumentNullException(nameof(item));
            EntityEntry<Book> book;

            try
            {
                book = context.Books.Update(item);
                await context.SaveChangesAsync();
            }
            catch
            {
                throw new InvalidOperationException("Book not found. Check provided identifier.");
            }


            if (!book.IsKeySet) { throw new InvalidDataException("Unknown error has occured"); }

            return book.Entity;
        }

        /// <summary>
        /// Get all
        /// </summary>
        /// <returns></returns>
        public DbSet<Book> GetAll()
        {
            return context.Books;
        }
    }
}
