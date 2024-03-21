using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace DatapacLibrary.DTO.Requests
{
    public class CreateBookRequest
    {
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
