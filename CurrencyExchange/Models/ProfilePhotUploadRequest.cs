namespace CurrencyExchange.Models
{
    public class ProfilePhotUploadRequest
    {
        public IFormFile? File { get; set; }
        public int UserId { get; set; }        
    }
}
