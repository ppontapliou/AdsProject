using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Serilog;
using Serviсes.Interfeces;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace UI.Controllers
{
    [Authorize]
    public class AdsController : Controller
    {
        private readonly ILogger _logger;
        private readonly IAdsService _adsServices;
        private readonly ICategoryService _categoryServices;
        private readonly IImageService _imageServices;
        private readonly IMapper _mapper;

        public AdsController(ILogger logger, IAdsService adsService, ICategoryService categoryService, IImageService imageService, IMapper mapper)
        {
            _logger = logger;
            _adsServices = adsService;
            _categoryServices = categoryService;
            _imageServices = imageService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetAds(Models.AdParameter ad = null)
        {

            if (ad is null)
            {
                ad = new Models.AdParameter();
            }
            Ads ads = _adsServices.GetAdsAsync(_mapper.Map<Ad>(ad));
            if (ads is null) { return NotFound(); }
            ViewData["Categories"] = ads.ListCategory;

            return View(ads.ListAd);
        }

        [HttpGet]
        public ActionResult GetAd(int id)
        {
            AdWithCategories ad = _adsServices.GetAdAsync(id);
            if (ad is null) { return NotFound(); }
            ViewData["Categories"] = ad.ListCategory;
            return View(ad.Ad);
        }

        [HttpGet]
        public IActionResult AddAd()
        {
            Models.Ad ad = new Models.Ad();
            ViewData["Categories"] = _categoryServices.GetCategories();
            return View(ad);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAd(Models.Ad ad, IFormFile file)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new ArgumentException();
                }
                ad.Image = _imageServices.SaveImage(file);
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _adsServices.AddAdAsync(_mapper.Map<Ad>(ad), Convert.ToInt32(userId));

                return RedirectToAction(nameof(GetAds),(ad));
            }
            catch (ArgumentException exception)
            {
                _logger.Error("addad – " + exception.Message);
                ViewData["Categories"] = _categoryServices.GetCategories();
                return View(ad);
            }
            catch (Exception exception)
            {
                _logger.Warning("addad – " + exception.Message);
                return StatusCode(503);
            }

        }

        [HttpGet]
        public ActionResult ChangeAd(int id)
        {
            AdWithCategories ad = _adsServices.GetAdAsync(id);
            if (ad is null) { return NotFound(); }

            ViewData["Categories"] = ad.ListCategory;
            return View(_mapper.Map<Models.Ad>(ad.Ad));

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeAd(Models.Ad ad, IFormFile file)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new ArgumentException();
                }
                if (file != null)
                {
                    ad.Image = _imageServices.SaveImage(file);
                }
                if (User.IsInRole("Admin") || User.IsInRole("Moderator"))
                {
                    _adsServices.ChangeAdAsync(_mapper.Map<Ad>(ad), ad.UserId);
                    return RedirectToAction(nameof(GetAds), ad);
                }

                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _adsServices.ChangeAdAsync(_mapper.Map<Ad>(ad), Convert.ToInt32(userId));

                return RedirectToAction(nameof(GetAds), (ad));
            }
            catch (ArgumentException exception)
            {
                _logger.Error("changead – " + exception.Message);
                ViewData["Categories"] = _categoryServices.GetCategories();
                return View(ad);
            }
            catch (Exception exception)
            {
                _logger.Warning("changead – " + exception.Message);
                return StatusCode(503);
            }
        }

        [HttpGet]
        public ActionResult DeleteAd(int id)
        {
            AdWithCategories ad = _adsServices.GetAdAsync(id);
            if (ad is null) { return NotFound(); }
            ViewData["Categories"] = ad.ListCategory;
            return View(ad.Ad);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAd(Ad ad)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new ArgumentException();
                }
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _adsServices.DeleteAdAsync(ad.Id, Convert.ToInt32(userId));

                return RedirectToAction(nameof(GetAds));
            }
            catch (ArgumentException exception)
            {
                _logger.Error("deletead – " + exception.Message);
                return NotFound();
            }
            catch (Exception exception)
            {
                _logger.Warning("deletead – " + exception.Message);
                return StatusCode(503);
            }

        }
    }
}
