using DatapacLibrary.DTO.Requests;
using DatapacLibrary.Models;

namespace DatapacLibrary.Interfaces
{
    public interface ILibraryService
    {
        /// <summary>
        /// Borrow book from library
        /// </summary>
        /// <param name="bookId"></param>
        /// <param name="userCardNumber"></param>
        /// <returns></returns>
        public Task<Checkout> BorrowBook(BorrowRequest request);
        /// <summary>
        /// Return borrowed book
        /// </summary>
        /// <param name="checkout"></param>
        public Task ReturnBook(ReturnRequest request);

    }
}
