using Azure.Storage.Blobs;
using ConnectionModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Models.Models;
using Serviсes.Interfeces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Serviсes.Services
{
    public class AzureImageService : IImageService
    {
        private readonly AzureSettings _azureSettings;
        public AzureImageService(IOptions<AzureSettings> azureSettings)
        {
            _azureSettings = azureSettings.Value;
        }
        public string SaveImage(IFormFile file)
        {
            string resultName = DateTime.Now.Millisecond + file.FileName;
            BlobClient blobClient = new BlobClient(_azureSettings.ConnectionString, _azureSettings.ImageContainerName, resultName);
            using(Stream stream = file.OpenReadStream())
            {
                blobClient.Upload(stream);
            }
            return "https://blobmsige.blob.core.windows.net/images/" + resultName;
        }
    }
}
