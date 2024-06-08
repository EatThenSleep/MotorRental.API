using Microsoft.AspNetCore.Http.HttpResults;

namespace MotorRental.Infrastructure.Presentation.Helper
{
    public static class FormFileExt
    {
        public static string SaveImage(this IFormFile? fileImage, string Id, string baseUrl)
        {
            string filename = Id + Path.GetExtension(fileImage.FileName);
            string filePath = @"wwwroot\ProductImages\" + filename;

            var directoryLocation = Path.Combine(Directory.GetCurrentDirectory(), filePath);

            FileInfo file = new FileInfo(directoryLocation);

            if (file.Exists)
            {
                file.Delete();
            }

            using (var fileStream = new FileStream(directoryLocation, FileMode.Create))
            {
                fileImage.CopyTo(fileStream);
            }

            return baseUrl + "/ProductImages/" + filename;
        }
    }
}
