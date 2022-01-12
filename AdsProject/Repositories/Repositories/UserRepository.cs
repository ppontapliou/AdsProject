using ConnectionModels;
using Hash;
using Microsoft.Extensions.Options;
using Models.Models;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Repositories.Repositories
{
    public class UserRepository : Repository, IUserRepository
    {
        private IHasher _hasher;
        public UserRepository(IOptions<DBConfig> config, IHasher hasher) : base(config.Value)
        {
            _hasher = hasher;
        }
        public void AddUser(User user)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", user.Id),
                new SqlParameter("@Name", user.UserName),
                new SqlParameter("@Role", user.Role),
                new SqlParameter("@Login", user.Login),
                new SqlParameter("@Password", _hasher.Hashing(user.Password)),
                new SqlParameter("@Phone", user.Phone),
                new SqlParameter("@Mail", user.Mail),
            };
            SendRequest("EXEC [AddUser] @Name, @Login, @Password, @Role, @Mail, @Phone", sqlParameters);
        }

        public void ChangeUser(User user)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", user.Id),
                new SqlParameter("@Login", user.Login),
                new SqlParameter("Name",user.UserName),
                new SqlParameter("@Phone", user.Phone),
                new SqlParameter("@Mail", user.Mail),
                new SqlParameter("@Role", user.Role)
            };
            SendRequest("EXEC ChangeUser @Id, @Name, @Role, @Login, @Phone, @Mail", sqlParameters);
        }
        public void ChangePassword(User user)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", user.Id),
                new SqlParameter("@Password", _hasher.Hashing(user.Password)),
            };
            SendRequest("EXEC ChangePassword @Id, @Password", sqlParameters);
        }
        public void DeleteUser(int id)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            SendRequest("EXEC DeleteUser @Id", sqlParameters);
        }  

        public User GetUser(int id)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
            };
            List<User> rezult = ReturnData<User>("EXEC GetUser @Id", sqlParameters).Result;
            if (rezult.Count == 1)
            {
                return rezult[0];
            }
            return null;
        }
        public User LoginUser(string login, string password)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@Login", login),
                new SqlParameter("@Password",_hasher.Hashing( password)),
            };
            List<User> users = ReturnData<User>("EXEC [LoginUser] @Login, @Password", sqlParameters).Result;
            if (users.Count == 1)
            {
                return users[0];
            }
            return null;
        }
        public List<User> GetUsers()
        {
            return ReturnData<User>("Exec GETUSERS").Result;
        }
    }
}
