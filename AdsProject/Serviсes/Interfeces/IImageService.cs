using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serviсes.Interfeces
{
    public interface IImageService
    {
        string SaveImage(IFormFile file);
    }
}
