using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourneyRent.BusinessLogic.Services;

namespace TourneyRent.Presentation.Api.Controllers
{
    [Route("Image")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly ImageService _imageService;

        public ImageController(ImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpGet("{imageFullName}")]
        public IActionResult GetImage(string imageFullName)
        {
            return File(_imageService.GetImageBytes(imageFullName), "image/jpeg");
        }
    }
}

