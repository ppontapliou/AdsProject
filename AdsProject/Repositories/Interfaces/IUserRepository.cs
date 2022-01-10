using Models.Models;
using System.Collections.Generic;

namespace Repositories.Interfaces
{
    public interface IUserRepository
    {
        public void DeleteUser(int id);
        public void AddUser(User user);
        public List<User> GetUsers();
        public User GetUser(int id);
        public void ChangeUser(User user);
        public void ChangePassword(User user);
        public User LoginUser(string login, string password);
    }
}
