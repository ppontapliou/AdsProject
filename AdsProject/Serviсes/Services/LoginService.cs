using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Models.Models;
using Repositories.Interfaces;
using Serilog;
using Serviсes.Interfeces;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;


namespace Serviсes.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;

        public LoginService(IUserRepository userRepository, ILogger logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task Login(HttpContext HttpContext, User user)
        {
            try
            {
                user = _userRepository.LoginUser(user.Login, user.Password);
                if (user is null) throw new ArgumentException();
                var claims = new List<Claim>()
                {
                   new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                   new Claim(ClaimTypes.Name, user.UserName),
                   new Claim(ClaimTypes.Role, ((Roles)user.Role).ToString()),
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var property = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, property);
            }
            catch (ArgumentException argumentException)
            {
                throw argumentException;
            }
            catch(Exception exception)
            {
                _logger.Error(exception.Message);
                throw exception;
            }
        }
        
        public async Task LogOut(HttpContext HttpContext)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
