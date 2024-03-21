using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace DatapacLibrary.DTO.Requests
{
    public class CreateUserRequest
    {
        [JsonProperty("firstName")]
        [StringLength(50)]
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [JsonProperty("lastName")]
        [StringLength(50)]
        [Required]
        public string LastName { get; set; } = string.Empty;
        [JsonProperty("email")]
        [RegularExpression(@"([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)")]
        [Required]
        public string Email { get; set; } = string.Empty;
    }
}
