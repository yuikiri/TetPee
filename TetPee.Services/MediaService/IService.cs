using Microsoft.AspNetCore.Http;

namespace TetPee.Services.MediaService;

public interface IService
{
    public Task<string> UploadImageAsync(IFormFile file);
}