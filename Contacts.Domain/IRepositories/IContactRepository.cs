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
        Task<bool> CreatePersonAsync(Person person);
        Task<bool> UpdatePersonAsync(Guid Id, Person person);

        Task<(Contact, IEnumerable<Contact>)> GetContactTreeAsync(Guid id);
        Task CreateContactAsync();

    }
}
