using Contacts.Domain.DbEntites;
using Contacts.Domain.DTO;
using Contacts.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Contacts.Persistence.Repositories
{
    internal class ContactRepository : IContactRepository
    {
        private readonly ContactsDatabaseContext _DbCon;

        public ContactRepository(ContactsDatabaseContext _DbCon)
            => this._DbCon = _DbCon;

        public Task CreateContactAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreatePersonAsync(Person person)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePersonAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<(Contact, IEnumerable<Contact>)> GetContactTreeAsync(Guid Id)
        {
            throw new NotImplementedException();
            //Contact ParentContact = default;
            //List<Contact> SubCuntacts;

            //var getContactId = await _DbCon.Contacts.FirstOrDefaultAsync(x => x.Id == Id);

            //if (getContactId != null)
            //{
            //    var parentContact = getContactId.ParentContactId;

            //    if (parentContact != null)
            //    {
            //        SubCuntacts = await _DbCon.Contacts.Where(x => x.ParentContactId == parentContact).Select(x => x).ToListAsync();
            //    }
            //    else
            //    {
                    
            //    }

            //}
        }

        public async Task<Person> GetPersonAsync(Guid Id)
            => await _DbCon.Persons.FirstOrDefaultAsync(x => x.Id == Id);


        public async Task<IEnumerable<PersonDisplay>> GetPersonOverallAsync()
        {
            IEnumerable<PersonDisplay> peoples = await _DbCon.Persons.Select(x => new PersonDisplay
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName
            }).ToListAsync();

            return peoples;
        }

        public Task<bool> UpdatePersonAsync(Guid Id, Person person)
        {
            throw new NotImplementedException();
        }

    }
}
