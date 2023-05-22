using Microsoft.AspNetCore.Http;
using TourneyRent.Contracts;
using TourneyRent.Contracts.Models;

namespace TourneyRent.DataLayer.Repositories;

public class ImageRepository
{
    private const string Storage = Constants.ImageStorage;

    public byte[] GetImageBytes(string imageFileName)
    {
        return File.ReadAllBytes($"{Storage}/{imageFileName}");
    }

    public async Task<Guid?> UploadImageAsync(IFormFile image, Guid? assingId = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var fileName = assingId ?? Guid.NewGuid();
            await using var stream = new FileStream($"{Storage}/{fileName}", FileMode.Create);
            await image.CopyToAsync(stream, cancellationToken);

            return fileName;
        }
        catch
        {
            return null;
        }
    }

    public async Task<Guid?> UploadImageAsync(IImageUpload image, Guid? assingId = null,
        CancellationToken cancellationToken = default)
    {
        return await UploadImageAsync(image.ImageFile, assingId, cancellationToken);
    }
}