using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatapacLibrary.Models
{
    public class Notification
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string? Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime SendAt { get; set; }
        public virtual Checkout Checkout { get; set; } = new();
    }
}
