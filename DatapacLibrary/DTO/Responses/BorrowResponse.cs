using Newtonsoft.Json;

namespace DatapacLibrary.DTO.Responses
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class BorrowResponse
    {
        [JsonProperty("checkoutId")]
        public int CheckoutId { get; set; }
        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; }
        [JsonProperty("errorMessage")]
        public string? Error { get; set; }
    }
}
