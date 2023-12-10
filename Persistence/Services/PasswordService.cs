using Contacts.Persistence.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

using BCrypt.Net;
using System.Security.Cryptography;

namespace Contacts.Persistence.Services
{
    internal class PasswordService : IPasswordService
    {
        public Task<string> GenerateHash(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return Task.Run(() => builder.ToString());
            }
        }

    }
}
