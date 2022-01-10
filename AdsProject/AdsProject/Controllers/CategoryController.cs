using System;
using System.Collections.Generic;
using System.Linq;
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
    public class CategoryController : Controller
    {
        private readonly ILogger _logger;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ILogger logger, ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            return View(_categoryService.GetCategories());
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCategory(Models.Category category)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new ArgumentException();
                }
                _categoryService.AddCategory(_mapper.Map<Parameter>(category));

                return Redirect("/Category/GetCategories");
            }
            catch (ArgumentException)
            {
                ViewBag.Message = "Invalid Values";
                return View();
            }
            catch (Exception)
            {
                return StatusCode(503);
            }
        }

        [HttpGet]
        public IActionResult ChangeCategory(int id)
        {
            Parameter result = _categoryService.GetCategories().FirstOrDefault(category => category.Id == id);
            if (result is null)
            {
                return NotFound();
            }
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeCategory(Models.Category category)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new ArgumentException();
                }
                _categoryService.ChangeCategory(_mapper.Map<Parameter>(category));
                return Redirect("/Category/GetCategories");
            }
            catch (ArgumentException)
            {
                ViewBag.Message = "Invalid Values";
                return View();
            }
            catch (Exception)
            {
                return StatusCode(503);
            }
        }

        [HttpGet]
        public IActionResult DeleteCategory(int id)
        {
            Parameter result = _categoryService.GetCategories().FirstOrDefault(category => category.Id == id);
            if (result is null)
            {
                return NotFound();
            }
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCategory(Parameter category)
        {
            try
            {
                _categoryService.DeleteCategory(category.Id);
                return Redirect("/Category/GetCategories");
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(503);
            }
        }
    }
}
