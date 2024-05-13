namespace CurrencyExchange.Interfaces
{
    public interface IProfilePhotoUpload
    {
        Task<string> UploadProfilePhoto(IFormFile file);
    }
}
