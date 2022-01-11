using Models.Models;
using Repositories;
using Serilog;
using Serviсes.Interfeces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Validator;

namespace Serviсes.Services
{
    public class AdsService : IAdsService
    {
        private IUnitOfWork _repositories;
        public AdsService(IUnitOfWork repositories)
        {
            _repositories = repositories;
        }

        public void AddAdAsync(Ad ad, int userId)
        {
            
            if (ad.IsValid())
            {
                ad.UserId = userId;
                _repositories.AdsRepository.AddAdAsync(ad);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public void ChangeAdAsync(Ad ad, int userId)
        {
            if (ad.IsValid())                
            {
                if (_repositories.AdsRepository.CheckAdAsync(ad.Id, userId).Result == 1)
                {
                    ad.UserId = userId;
                    _repositories.AdsRepository.ChangAdAsync(ad);
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            else
            {
                throw new ArgumentException();
            }

        }

        public void DeleteAdAsync(int id, int UserId)
        {
            if (_repositories.AdsRepository.CheckAdAsync(id, UserId).Result == 1)
            {
                _repositories.AdsRepository.DeleteAdAsync(id);
                return;
            }
            else { throw new ArgumentException(); }

        }

        public AdWithCategories GetAdAsync(int id)
        {
            AdWithCategories result = new AdWithCategories();

            if (id > 0)
            {
                Task<Ad> taskAd = _repositories.AdsRepository.GetAdAsync(id);
                Task<List<Parameter>> taskCategory = _repositories.CategoryRepository.GetCategoriesAsync();

                Task.WhenAll(taskAd, taskCategory);

                result.Ad = taskAd.Result;
                result.ListCategory = taskCategory.Result;

                return result;
            }
            return null;

        }

        public Ads GetAdsAsync(Ad ad)
        {
            Ads rezult = new Ads();
            Task<List<Ad>> taskAds = _repositories.AdsRepository.GetAdsAsync(ad);
            Task<List<Parameter>> taskCategory = _repositories.CategoryRepository.GetCategoriesAsync();

            Task.WhenAll(taskAds, taskCategory);

            rezult.ListAd = taskAds.Result;
            rezult.ListCategory = taskCategory.Result;

            return rezult;
        }
    }
}
