using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Serviсes.Interfeces
{
    public interface ILoginService
    {
        public Task Login(HttpContext HttpContext, User user);
        public Task LogOut(HttpContext HttpContext);
    }
}
