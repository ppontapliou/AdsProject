using Models.Models;
using Repositories.Interfaces;
using Serilog;
using Serviсes.Interfeces;
using System;
using System.Collections.Generic;
using Validator;

namespace Serviсes.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void AddUser(User user)
        {
            if (user.IsValid())
            {
                _userRepository.AddUser(user);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public void ChangeUser(User user)//login Name Password role 
        {
            if (user.IsValid() &&
                user.Id > 0)
            {
                _userRepository.ChangeUser(user);
            }
            else
            {
                throw new ArgumentException();
            }
        }
        public void ChangePassword(User user)
        {
            if (user.IsValidPassword() ||
                user.Id > 0)
            {
                _userRepository.ChangePassword(user);
            }
            else
            {
                throw new ArgumentException();
            }
        }
        public void DeleteUser(int id)
        {
            if (id > 0) { _userRepository.DeleteUser(id); }
            else { throw new ArgumentException(); }
        }


        public User GetUser(int id)
        {
            if (id > 0)
            { return _userRepository.GetUser(id); }
            else { throw new ArgumentException(); }
        }

        public List<User> GetUsers()
        {
            return _userRepository.GetUsers();
        }
    }
}
