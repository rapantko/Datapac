using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace DatapacLibrary.DTO.Requests
{
    public class UpdateBookRequest
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("title")]
        [StringLength(50)]
        [Required]
        public string Title { get; set; } = string.Empty;
        [JsonProperty("author")]
        [StringLength(50)]
        [Required]
        public string Author { get; set; } = string.Empty;
    }
}
