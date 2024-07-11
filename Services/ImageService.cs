using Library_Management_System_BackEnd.Helper.Response;
using Library_Management_System_BackEnd.Interfaces;

namespace Library_Management_System_BackEnd.Services
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public ImageService(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        public async Task<SavaImageRespoce> SaveImageAsync(IFormFile imageFile)
        {
            try
            {
                var rootPath = _hostEnvironment.ContentRootPath;
                var Uploads = Path.Combine(rootPath, "Uploads");
                if (!Directory.Exists(Uploads))
                {
                    Directory.CreateDirectory(Uploads);
                }
                var ImageExtension = imageFile.FileName.Substring(
                    imageFile.FileName.LastIndexOf('.')
                );
                if (!IsValidImage(ImageExtension))
                {
                    return new SavaImageRespoce
                    {
                        IsSuccess = false,
                        Error = new InvalidOperationException("Invalid Image Extension")
                    };
                }

                var FileName = GenerateUniqueName(ImageExtension);

                var FilePath = Path.Combine(Uploads, FileName);
                using var stream = new FileStream(FilePath, FileMode.Create);
                await imageFile.CopyToAsync(stream);

                await stream.DisposeAsync();

                return new SavaImageRespoce
                {
                    ImageName = $"Resource//{FileName}",
                    IsSuccess = true
                };
            }
            catch (System.Exception e)
            {
                return new SavaImageRespoce { IsSuccess = false, Error = e };
            }
        }

        private string GenerateUniqueName(string imageExtension)
        {
            return $"{Guid.NewGuid()}{imageExtension}";
        }

        public static bool IsValidImage(string imageExtension)
        {
            var ValidExitention = new string[] { ".jpg", ".jpeg", ".png", ".gif" };
            return ValidExitention.Contains(imageExtension.ToLower());
        }
    }
}
