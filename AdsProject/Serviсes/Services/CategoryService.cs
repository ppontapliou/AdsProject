using Models.Models;
using Repositories.Interfaces;
using Serilog;
using Serviсes.Interfeces;
using System;
using System.Collections.Generic;
using Validator;

namespace Serviсes.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public void AddCategory(Parameter category)
        {
            if (category.IsValid())
            {
                _categoryRepository.AddCategory(category);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public void ChangeCategory(Parameter category)
        {
            if (category.IsValid() && category.Id > 0)
            {
                _categoryRepository.ChangeCategory(category);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public void DeleteCategory(int id)
        {
            if (id > 0)
            {
                _categoryRepository.DeleteCategory(id);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public List<Parameter> GetCategories()
        {
            return _categoryRepository.GetCategoriesAsync().Result;
        }
    }
}
