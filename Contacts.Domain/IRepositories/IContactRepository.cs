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
        Task<IEnumerable<PersonDisplay>> GetPersonOverallAsync();
        Task<Person> GetPersonAsync(Guid Id);
        Task<bool> DeletePersonAsync(Guid Id);
        Task<int> CrupDatePersonAsync(Guid Id, PersonDTO person);

        Task<(ContactDTO, IEnumerable<ContactDTO>)> GetContactTreeAsync(Guid id);

    }
}
