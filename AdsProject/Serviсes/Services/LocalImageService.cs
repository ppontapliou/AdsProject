using Microsoft.AspNetCore.Http;
using Serviсes.Interfeces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Serviсes.Services
{
    public class LocalImageService : IImageService
    {
        private List<string> ContentTypes = new List<string> { "image/jpg", "image/jpeg", "image/pjpeg", "image/gif", "image/x-png", "image/png", };
        public string SaveImage(IFormFile file)
        {
            if (file == null || file.Length == 0) throw new ArgumentException("File is empty");
            if (!ContentTypes.Contains(file.ContentType)) throw new ArgumentException("It's not image");
            
            string resultPath = "/Images/" + DateTime.Now.Millisecond + file.FileName;
            string savePath = "wwwroot"+resultPath;
            using (var stream = new FileStream(savePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return resultPath;
        }
    }
}
