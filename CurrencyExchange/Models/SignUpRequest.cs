using System.ComponentModel.DataAnnotations;

namespace CurrencyExchange.Models
{
    public class SignUpRequest
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        [Phone]
        public string? PhoneNumber { get; set; }
    }
}
