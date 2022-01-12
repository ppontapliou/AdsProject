﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Serilog;
using Serviсes.Interfeces;

namespace UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly ILogger _logger;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(ILogger logger, IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            return View(_userService.GetUsers());
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddUser(Models.User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new ArgumentException();
                }
                _userService.AddUser(_mapper.Map<User>(user));
                return Redirect("/User/GetUsers");
            }
            catch (ArgumentException exception)
            {
                _logger.Error("Adduser – " + exception.Message);
                ViewBag.Message = "Некорректные значения";
                return View();
            }
            catch (Exception exception)
            {
                _logger.Warning("Adduser – " + exception.Message);
                return StatusCode(503);
            }
        }

        [HttpGet]
        public IActionResult ChangeUser(int id)
        {
            if (id < 1) { return BadRequest(); }

            User user = _userService.GetUser(id);

            if (user is null) { return NotFound(); }

            return View(_mapper.Map<Models.User>(user));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeUser(Models.User user)
        {
            try
            {
                ModelState["Password"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Skipped;
                if (!ModelState.IsValid)
                {
                    throw new ArgumentException();
                }

                _userService.ChangeUser(_mapper.Map<User>(user));
                return Redirect("/User/GetUsers");

            }
            catch (ArgumentException exception)
            {
                _logger.Error("Changeuser – " + exception.Message);
                ViewBag.Message = "Некорректные значения";
                return View(user);
            }
            catch (Exception exception)
            {
                _logger.Warning("Changeuser – " + exception.Message);
                return StatusCode(503);
            }
        }

        [HttpGet]
        public IActionResult DeleteUser(int id)
        {
            User user = _userService.GetUser(id);
            if (user is null)
            {
                return NotFound();
            }
            return View(_userService.GetUser(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteUser(User user)
        {
            try
            {
                _userService.DeleteUser(user.Id);

                return Redirect("/User/GetUsers");
            }
            catch (ArgumentException exception)
            {
                _logger.Error("deleteuser – " + exception.Message);
                return NotFound();
            }
            catch (Exception exception)
            {
                _logger.Warning("deleteuser – " + exception.Message);
                return StatusCode(503);
            }
        }

        [HttpGet]
        public IActionResult GetUser(int id)
        {
            User user = _userService.GetUser(id);
            if (user is null)
            {
                return NotFound();
            }
            return View(_userService.GetUser(id));
        }
    }
}
