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

        public async Task<bool> DeletePersonAsync(Guid Id)
        {
            var getPrsn = await GetPersonAsync(Id);

            if (getPrsn == null)
                return false;

            try
            {
                _DbCon.Persons.Remove(getPrsn);
                await _DbCon.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<(ContactDTO, IEnumerable<ContactDTO>)> GetContactTreeAsync(Guid Id)
        {
            var checkForAnyContact = await _DbCon.Contacts.FirstOrDefaultAsync(x => x.Id == Id);

            if (checkForAnyContact is null)
                return (null, null);

            var getCascadeContact = await _DbCon.Contacts.FirstOrDefaultAsync(x => x.ParentContactId == Id);

            if(getCascadeContact is null)
            {
                var getContact = await _DbCon.Contacts.FirstOrDefaultAsync(x => x.Id == Id);

                var contactDTO = new ContactDTO()
                {
                    Id = getContact.Id,
                    Name = getContact.Name
                };

                return (contactDTO, null);
            }

            var getParentContact = await _DbCon.Contacts.FirstAsync(x => x.Id == (Guid)getCascadeContact.ParentContactId);

            var cascadeContactIntoDTO = new ContactDTO
            {
                Id = getParentContact.Id,
                Name = getParentContact.Name
            };

            var getSubContacts = await _DbCon.Contacts.Where(x => x.ParentContactId == Id).Select(y => 
                new ContactDTO
                {
                    Id = y.Id,
                    Name = y.Name
                }).ToListAsync();

            return (cascadeContactIntoDTO, getSubContacts);
        }

        public async Task<Person> GetPersonAsync(Guid Id)
            => await _DbCon.Persons.Include(y => y.Contact).ThenInclude(z => z.ContactRel).FirstOrDefaultAsync(x => x.Id == Id);


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

        public async Task<int> CrupDatePersonAsync(Guid Id, PersonDTO person)
        {
            //0 - error
            //1 - created
            //2 - updated


            var getPerson = await GetPersonAsync(Id);

            if (getPerson is null)
                return 0;

            var getCascadeContact = await _DbCon.Contacts.FirstOrDefaultAsync(x => x.ParentContactId == person.ParentContactId
                && x.Id == person.SubContactId);

            //update

            if (getCascadeContact != null)
            {
                getPerson.ContactId = getCascadeContact.Id;

                try
                {
                    await _DbCon.SaveChangesAsync();
                    return 2;
                }
                catch
                {
                    return 0;
                }

            }

            //create

            var getParentContact = await _DbCon.Contacts.FirstOrDefaultAsync(x => x.Id == person.ParentContactId && x.ParentContactId == null);

            var newContact = new Contact()
            {
                Id = person.SubContactId,
                ParentContactId = getParentContact.Id,
                Name = person.SubContactName
            };

            var newPerson = new Person()
            {
                Id = person.Id,
                Email = person.Email,
                DateOfBirth = person.DateOfBirth,
                Password = person.Password,
                FirstName = person.FirstName,
                LastName = person.LastName,
                PhoneNumber = person.PhoneNumber,
                Contact = newContact
            };

            try
            {
                 _DbCon.Persons.Add(newPerson);
                await _DbCon.SaveChangesAsync();
                return 1;
            }
            catch
            {
                return 0;
            }
        }

    }
}
