using Microsoft.AspNetCore.Http;

namespace TourneyRent.Contracts.Models
{
    public interface IImageUpload
    {
        IFormFile ImageFile { get; set; }
    }
}
