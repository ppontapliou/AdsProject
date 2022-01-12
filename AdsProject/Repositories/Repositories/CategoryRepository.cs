using ConnectionModels;
using Microsoft.Extensions.Options;
using Models.Models;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class CategoryRepository : Repository, ICategoryRepository
    {
        public CategoryRepository(IOptions<DBConfig> config) : base(config.Value)
        {

        }

        public void AddCategory(Parameter category)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@Category", category.Name),
            };
            SendRequest("EXEC AddCategory @Category", sqlParameters);
        }

        public void ChangeCategory(Parameter category)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", category.Id),
                new SqlParameter("@Category", category.Name),
            };
            SendRequest("EXEC ChangeCategory @Id, @Category", sqlParameters);
        }

        public void DeleteCategory(int id)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            SendRequest("EXEC DeleteCategory @Id", sqlParameters);
        }

        public Task<List<Parameter>> GetCategoriesAsync()
        {
            return ReturnData<Parameter>("EXEC GETCATEGORies");
        }
    }
}
