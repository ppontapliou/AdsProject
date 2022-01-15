using Models.Models;
using System.Collections.Generic;

namespace Serviсes.Interfeces
{
    public interface IUserService
    {
        public void DeleteUser(int id);
        public void ChangeUser(User user);
        public void ChangePassword(User user);
        public void AddUser(User user);
        public List<User> GetUsers();
        public User GetUser(int id);
    }
}
