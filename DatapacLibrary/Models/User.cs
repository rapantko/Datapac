using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DatapacLibrary.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [JsonIgnore]
        public int CardNumber { get; set; }
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;
        [RegularExpression("^[A-Z0-9._%+-]+@[A-Z0-9.-]+\\.[A-Z]{2,}$")]
        public string Email { get; set; } = string.Empty;
        public bool IsBlocked { get; set; }

    }
}