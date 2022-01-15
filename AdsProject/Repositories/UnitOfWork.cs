using Microsoft.Extensions.Options;
using Models.Models;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IAdsRepository AdsRepository { get; set; }
        public IUserRepository UserRepository { get; set; }
        public ICategoryRepository CategoryRepository { get; set; }
      

        public UnitOfWork(IAdsRepository adsRepository, IUserRepository userRepository, ICategoryRepository categoryRepository)
        {
           AdsRepository = adsRepository;
           UserRepository = userRepository;
           CategoryRepository = categoryRepository;          
        }
    }
}
