using Contacts.Domain.DTO;
using Contacts.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Services
{
    public class PersonService : IPersonService
    {
        private readonly HttpClient _httpCli;

        public PersonService(HttpClient _httpCli)
        {
            this._httpCli = _httpCli;
        }

        public async Task<IEnumerable<PersonDisplay>> GetPersons()
        {
            
        }
    }
}
