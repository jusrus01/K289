using TourneyRent.Contracts;
using TourneyRent.Contracts.Models;

namespace TourneyRent.DataLayer.Repositories;

public class ImageRepository
{
    private const string Storage = Constants.ImageStorage;

    public byte[] GetImageBytes(string imageFileName)
    {
        // return File.ReadAllBytes($"{Storage}/{imageFileName}");
        return new byte[] { };
    }

    public async Task<Guid?> UploadImageAsync(IImageUpload image, Guid? assingId = null)
    {
        try
        {
            var fileName = assingId ?? Guid.NewGuid();
            using (var stream = new FileStream($"{Storage}/{fileName}", FileMode.Create))
            {
                await image.ImageFile.CopyToAsync(stream);
            }

            return fileName;
        }
        catch
        {
            return null;
        }
    }
}