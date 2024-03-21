using Microsoft.EntityFrameworkCore;

namespace DatapacLibrary.Interfaces
{
    /// <summary>
    /// Generic interface that takes care of article administration
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IArticleRepository<T> where T : class
    {
        /// <summary>
        /// Get article by its unique identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<T?> GetById(int id);
        /// <summary>
        /// Create new article
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Task<T> Create(T item);
        /// <summary>
        /// Update already existing article
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Task<T> Update(T item);
        /// <summary>
        /// Delete article
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Task Remove(T item);
        /// <summary>
        /// Get all
        /// </summary>
        /// <returns></returns>
        public DbSet<T> GetAll();

    }
}
