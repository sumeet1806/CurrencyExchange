using System.ComponentModel.DataAnnotations;

namespace CurrencyExchange.Models
{
    public class SignInRequest
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
