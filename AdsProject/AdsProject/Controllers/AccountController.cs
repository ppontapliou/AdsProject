using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Serviсes.Interfeces;
using Serilog;
using System;
using System.Security.Claims;
using AutoMapper;

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
        public IActionResult Login()
        {
            Models.LoginData objLoginModel = new Models.LoginData();
            return View(objLoginModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(Models.LoginData user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _loginSercice.Login(HttpContext, _mapper.Map<User>(user));
                    return LocalRedirect("~/Ads/GetAds");
                }
                throw new ArgumentException();
            }
            catch (ArgumentException)
            {
                ViewBag.Message = "Неверные данные";
                return View(user);
            }
            catch (Exception)
            {
                return StatusCode(503);
            }
        }

        [HttpGet]
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
            catch (ArgumentException)
            {
                ViewBag.Message = "Ошибка";
                return View(user);
            }
            catch (Exception)
            {
                return StatusCode(503);
            }
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            int id = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User user = _userService.GetUser(id);

            if (user is null) { return BadRequest(); }
            return View(_mapper.Map<Models.ChangePasswordModel>(user));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
            catch (ArgumentException)
            {
                ViewBag.Message = "Ошибка";
                return View(user);
            }
            catch (Exception)
            {
                return StatusCode(503);
            }
        }
    }
}
