using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatapacLibrary.Models
{
    public class Checkout
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartTime { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndTime { get; set; }
        public virtual Book Book { get; set; } = new();
        public virtual User User { get; set; } = new();
        public bool IsReturned { get; set; }
    }
}
