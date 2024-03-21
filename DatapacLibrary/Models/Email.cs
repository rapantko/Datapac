using System.ComponentModel.DataAnnotations;

namespace DatapacLibrary.Models
{
    /// <summary>
    /// Email message base class
    /// </summary>
    public class Email
    {
        [RegularExpression(@"([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)")]
        public string Recipient { get; set; } = string.Empty;
        [StringLength(30)]
        public string Subject { get; set; } = string.Empty;
        [StringLength(100)]
        public string Body { get; set; } = string.Empty;

    }
}
