using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading.Tasks;
using ConnectionModels;
using Microsoft.Extensions.Options;
using Models.Models;

namespace Repositories
{
    public abstract class Repository
    {
        private readonly string _connectionString;
        public Repository(DBConfig option)
        {
            _connectionString = option.ConnectionString;
        }
        protected async Task<List<T>> ReturnData<T>(string sqlExpression, List<SqlParameter> sqlParameters = null) where T : class, new()
        {
            List<T> rezultList = new List<T>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    if (sqlParameters != null)
                        foreach (var param in sqlParameters)
                        {
                            command.Parameters.Add(param);
                        }
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            rezultList.Add(ReflectParameter<T>(reader));
                        }
                    }
                }
            }
            return rezultList;
        }
        protected async Task<int> ReturnScalar(string sqlExpression, List<SqlParameter> sqlParameters = null)
        {
            int rezult = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    if (sqlParameters != null)
                        foreach (var param in sqlParameters)
                        {
                            command.Parameters.Add(param);
                        }
                    rezult = (int)await command.ExecuteScalarAsync();
                }
            }
            return rezult;
        }
        protected void SendRequest(string sqlExpression, List<SqlParameter> sqlParameters = null)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    foreach (var param in sqlParameters)
                    {
                        command.Parameters.Add(param);
                    }
                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }
        }
        private T ReflectParameter<T>(SqlDataReader reader) where T : class, new()
        {
            Type t = typeof(T);
            int countColumn = reader.FieldCount;
            T rezultObject = new T();
            string columnName;

            for (int i = 0; i < countColumn; i++)
            {
                columnName = reader.GetName(i);
                t.GetProperty(columnName).SetValue(rezultObject, reader[columnName]);
            }

            return rezultObject;
        }
    }
}
