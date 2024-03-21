
using Newtonsoft.Json;

namespace DatapacLibrary.DTO
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class BookResponse
    {
        [JsonProperty("id")]
        public int BookID { get; set; }
        [JsonProperty("title")]
        public string? Title { get; set; }
        [JsonProperty("author")]
        public string? Author { get; set; }
        [JsonProperty("errorMessage")]
        public string? Error { get; set; }
    }
}
