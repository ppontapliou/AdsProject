using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IAdsRepository
    {
        public Task<List<Ad>> GetAdsAsync(Ad ad);
        public Task<Ad> GetAdAsync(int id);
        public void AddAdAsync(Ad ad);
        public void ChangAdAsync(Ad ad);
        public void DeleteAdAsync(int id);
        public Task<int> CheckAdAsync(int id, int userId);
    }
}
