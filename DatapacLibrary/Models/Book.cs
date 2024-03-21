using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DatapacLibrary.Models
{
    /// <summary>
    /// Book base class
    /// </summary>
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int Id { get; set; }
        [StringLength(50)]
        [Required]
        public string Title { get; set; } = string.Empty;
        [StringLength(50)]
        [Required]
        public string Author { get; set; } = string.Empty;

    }
}
