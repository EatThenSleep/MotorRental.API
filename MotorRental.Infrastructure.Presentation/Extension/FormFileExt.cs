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

        public static void DeleteImage(Guid MotorId, string url)
        {
            string extFile = string.Empty;

            for(int i = url.Length - 1; i >= 0; i--)
            {
                extFile = url.ElementAt(i) + extFile;
                if (url.ElementAt(i) == '.') break;
            }

            var ImageLocalPath = $"wwwroot\\ProductImages\\{MotorId}{extFile}";
            var oldFilePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), ImageLocalPath);

            FileInfo file = new FileInfo(oldFilePathDirectory);
            if (file.Exists)
            {
                file.Delete();
            }
        }
    }
}
