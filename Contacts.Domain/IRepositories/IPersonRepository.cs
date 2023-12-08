using Contacts.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Domain.IRepositories
{
    public interface IPersonRepository
    {
        Task<IEnumerable<PersonDisplay>> GetPersonOverallAsync();
        Task<PersonDTO> GetPersonAsync(Guid Id);
        Task<bool> DeletePersonAsync(Guid Id);
        Task<int> CrupDatePersonAsync(Guid Id, PersonDTO person);
    }
}
