using Contacts.Domain.DbEntites;
using Contacts.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Domain.Repositories
{
    public interface IContactRepository
    {
        Task<(ContactDTO, IEnumerable<ContactDTO>)> GetContactTreeAsync(Guid id);
        Task<int> CreateContact(string name);

    }
}
