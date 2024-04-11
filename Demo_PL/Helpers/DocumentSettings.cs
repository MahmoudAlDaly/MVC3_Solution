using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Demo_PL.Helpers
{
    public static class DocumentSettings
    {
        public static async Task<string> UploadFile(IFormFile file,string foldename)
        {
            // 1- get located folder path
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(),"WWWroot\\files",foldename);

            if (Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // 2- get file name and make it unique
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            // 3- get file path
            string filePath = Path.Combine(folderPath,fileName);

            // 4- save file as stremas [data pet time]
            using FileStream fileStream = new FileStream(filePath,FileMode.Create);

            await file.CopyToAsync(fileStream);

            return fileName;
        }

        public static void DeleteFile(string fileName,string folderName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "WWWroot\\files", folderName,fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
