using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        public void DeleteCategory(int id);
        public void ChangeCategory(Parameter category);
        public void AddCategory(Parameter category);
        public Task<List<Parameter>> GetCategoriesAsync();
    }
}
