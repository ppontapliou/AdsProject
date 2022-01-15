using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;

namespace Serviсes.Interfeces
{
    public interface IAdsService
    {
        public Ads GetAdsAsync(Ad ad);
        public AdWithCategories GetAdAsync(int id);
        public void AddAdAsync(Ad ad, int userId);
        public void ChangeAdAsync(Ad ad, int UserId);
        public void DeleteAdAsync(int id, int UserId);
    }
}
