using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace DatapacLibrary.DTO.Requests
{
    public class ReturnRequest
    {
        [JsonProperty("checkoutId")]
        [Required]
        public int CheckoutId { get; set; }
        [JsonProperty("userCardNumber")]
        [Required]
        public int UserCardNumber { get; set; }
    }
}
