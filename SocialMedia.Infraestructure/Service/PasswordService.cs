using Microsoft.Extensions.Options;
using SocialMedia.Infraestructure.Interfaz;
using SocialMedia.Infraestructure.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infraestructure.Service
{
    public class PasswordService : IPasswordService
    {
        private readonly PasswordOptions _options;
        public PasswordService( IOptions<PasswordOptions> options)
        {
            _options = options.Value;
        }

        public bool Check(string hash, string password)
        {
            var parts = hash.Split('.');
            if (parts.Length != 3)
            {
                throw new FormatException("Unexpected has format");
            }
            var iterations= Convert.ToInt32(parts[0]);
            var salt= Convert.FromBase64String(parts[1]);
            var key=Convert.FromBase64String(parts[2]);
            using (var algoritm=new Rfc2898DeriveBytes(
                password,
                salt,
                iterations))
            {
                var keyChecked = algoritm.GetBytes(_options.KeySize);
                return keyChecked.SequenceEqual(key);
            }
        }

        public string Hash(string password)
        {
            //PBKDF2 algoritmo de implementacion
            using (var algoritmo=new Rfc2898DeriveBytes(
                password,
                _options.SaltSize,
                _options.Iteration))
            {
                var key =Convert.ToBase64String(algoritmo.GetBytes(_options.KeySize));
                var salt = Convert.ToBase64String(algoritmo.Salt);
                return $"{_options.Iteration}.{salt}.{key}";
            }
        }
    }
}
