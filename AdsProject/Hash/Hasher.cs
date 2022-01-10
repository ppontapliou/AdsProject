using ConnectionModels;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;
using System;
using System.Text;

namespace Hash
{
    public class Hasher : IHasher
    {
        private readonly HashConfig _config;
        public Hasher(IOptions<HashConfig> config)
        {
            _config = config.Value;
        }
        public string Hashing(string password)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Encoding.UTF8.GetBytes(_config.Salt),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 1000,
                numBytesRequested: 256 / 8));
        }
    }
}

