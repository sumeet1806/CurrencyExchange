using CurrencyExchange.Interfaces;
using CurrencyExchange.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProfilePhotoController : ControllerBase
{
    private readonly IProfilePhotoUpload _upload;

    public ProfilePhotoController(IProfilePhotoUpload upload)
    {
        _upload = upload;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadProfilePhoto([FromForm] ProfilePhotUploadRequest request)
    {
        if (request.File == null || request.File.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        string uploadedFileName = await _upload.UploadProfilePhoto(request.File);
        
        return Ok(new { FileName = uploadedFileName });
    }
}
