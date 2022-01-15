using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories
{
    public interface IUnitOfWork
    {
        public IAdsRepository AdsRepository { get; set; }
        public IUserRepository UserRepository { get; set; }
        public ICategoryRepository CategoryRepository { get; set; }
    }
}
