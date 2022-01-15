using Models.Models;
using System.Collections.Generic;

namespace Serviсes.Interfeces
{
    public interface ICategoryService
    {
        public void DeleteCategory(int id);
        public void ChangeCategory(Parameter category);
        public void AddCategory(Parameter category);
        public List<Parameter> GetCategories();

    }
}
