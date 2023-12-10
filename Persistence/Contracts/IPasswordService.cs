using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Persistence.Contracts
{
    public interface IPasswordService
    {
        Task<string> GenerateHash(string password);
    }
}
