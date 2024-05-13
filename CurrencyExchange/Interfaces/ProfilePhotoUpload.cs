namespace CurrencyExchange.Interfaces
{
    public class ProfilePhotoUpload : IProfilePhotoUpload
    {
        public async Task<string> UploadProfilePhoto(IFormFile file)
        {
            return file.FileName;
        }
    }
}
