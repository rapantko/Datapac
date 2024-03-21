using Newtonsoft.Json;

namespace DatapacLibrary.DTO
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class UserResponse
    {
        [JsonProperty("cardNumber")]
        public int CardNumber { get; set; }
        [JsonProperty("firstName")]
        public string? FirstName { get; set; }
        [JsonProperty("lastName")]
        public string? LastName { get; set; }
        [JsonProperty("email")]
        public string? Email { get; set; }
        [JsonProperty("errorMessage")]
        public string? Error { get; set; }
    }
}
