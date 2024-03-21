using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace DatapacLibrary.DTO.Requests
{
    public class BorrowRequest
    {
        [JsonProperty("bookId")]
        [Required]
        public int BookId { get; set; }
        [JsonProperty("userCardNumber")]
        [Required]
        public int UserCardNumber { get; set; }
    }
}
