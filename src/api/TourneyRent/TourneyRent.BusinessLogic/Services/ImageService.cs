using TourneyRent.DataLayer.Repositories;

namespace TourneyRent.BusinessLogic.Services
{
    public class ImageService
    {
        private readonly ImageRepository _imageRepository;

        public ImageService(ImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public byte[] GetImageBytes(string imageFileName)
        {
            return _imageRepository.GetImageBytes(imageFileName);
        }
    }
}
