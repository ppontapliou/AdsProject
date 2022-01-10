using System;
using System.Collections.Generic;
using System.Text;

namespace Hash
{
    public interface IHasher
    {
        public string Hashing(string password);
    }
}
