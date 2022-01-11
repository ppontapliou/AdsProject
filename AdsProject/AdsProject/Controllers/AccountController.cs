using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Serviсes.Interfeces;
using Serilog;
using System;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILoginService _loginSercice;
        private readonly ILogger _logger;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AccountController(ILoginService loginService, IUserService userService, ILogger logger, IMapper mapper)
        {
            _loginSercice = loginService;
            _logger = logger;
            _userService = userService;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Login()
        {
            Models.LoginData objLoginModel = new Models.LoginData();
            return View(objLoginModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Models.LoginData user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _loginSercice.Login(HttpContext, _mapper.Map<User>(user));
                    return LocalRedirect("/Ads/GetAds");
                }
                throw new ArgumentException();
            }
            catch (ArgumentException exception)
            {
                _logger.Error("Login – " + exception.Message);
                ViewBag.Message = "Неверные данные";
                return View(user);
            }
            catch (Exception exception)
            {
                _logger.Warning("Login – " + exception.Message);
                return StatusCode(503);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await _loginSercice.LogOut(HttpContext);
            return LocalRedirect("/Account/Login");
        }

        [HttpGet]
        public IActionResult ChangeAccount()
        {
            int id = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User user = _userService.GetUser(id);
            if (user is null)
            {
                return BadRequest();
            }
            return View(_mapper.Map<Models.User>(user));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult ChangeAccount(Models.User user)
        {
            try
            {
                ModelState["Role"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Skipped;
                if (!ModelState.IsValid)
                {
                    throw new ArgumentException();
                }

                user.Id = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

                _userService.ChangeUser(_mapper.Map<User>(user));

                return Redirect("/Ads/GetAds");
            }
            catch (ArgumentException exception)
            {
                _logger.Error("changeaccount – " + exception.Message);
                ViewBag.Message = "Ошибка";
                return View(user);
            }
            catch (Exception exception)
            {
                _logger.Warning("changeaccount – " + exception.Message);
                return StatusCode(503);
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult ChangePassword()
        {
            int id = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User user = _userService.GetUser(id);

            if (user is null) { return BadRequest(); }
            return View(_mapper.Map<Models.ChangePasswordModel>(user));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult ChangePassword(Models.ChangePasswordModel user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new ArgumentException();
                }
                else
                {
                    user.Id = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                    _userService.ChangePassword(_mapper.Map<User>(user));
                }
                return Redirect("/User/GetUsers");
            }
            catch (ArgumentException exception)
            {
                _logger.Error("changepassword – " + exception.Message);
                ViewBag.Message = "Ошибка";
                return View(user);
            }
            catch (Exception exception)
            {
                _logger.Warning("changepassword – " + exception.Message);
                return StatusCode(503);
            }
        }

        [HttpGet]
        public IActionResult Registrate()
        {
            Models.User user = new Models.User();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registrate(Models.User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new ArgumentException();
                }
                else
                {
                    user.Role = (int)Roles.User;
                    _userService.AddUser(_mapper.Map<User>(user));
                }
                return Redirect("/Account/Login");
            }
            catch (ArgumentException exception)
            {
                _logger.Error("Registrate – " + exception.Message);
                ViewBag.Message = "Ошибка";
                return View(user);
            }
            catch (Exception exception)
            {
                _logger.Warning("Registrate – " + exception.Message);
                return StatusCode(503);
            }
        }

    }
}
