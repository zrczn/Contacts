using Contacts.Domain.DbEntites;
using Contacts.Domain.DTO;
using Contacts.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Contacts.Persistence.Repositories
{
    internal class ContactRepository : AbstractRepository, IContactRepository
    {
        public ContactRepository(ContactsDatabaseContext _DbCon) : base(_DbCon)
        {
        }

        public async Task<int> CreateContact(string name)
        {
            //0 - bad entry
            //1 - already exists
            //2 - internal error
            //3 - ok

            if (string.IsNullOrEmpty(name))
                return 0;

            var doesNameExists = await _DbCon.Contacts.FirstOrDefaultAsync(x => x.Name == name);

            if (doesNameExists != null)
                return 1;

            Contact getParentContact = await _DbCon.Contacts.FirstOrDefaultAsync(x => x.Name == "inny" && x.ParentContactId == null);


            Contact newContact = new()
            {
                Name = name,
                ParentContactId = getParentContact.Id
            };

            try
            {
                _DbCon.Contacts.Add(newContact);
                await _DbCon.SaveChangesAsync();
                return 3;
            }
            catch
            {
                return 2;
            }
        }

        public async Task<(ContactDTO, IEnumerable<ContactDTO>)> GetContactTreeAsync(Guid Id)
        {
            var checkForAnyContact = await _DbCon.Contacts.FirstOrDefaultAsync(x => x.Id == Id);

            if (checkForAnyContact is null)
                return (null, null);

            var getCascadeContact = await _DbCon.Contacts.FirstOrDefaultAsync(x => x.ParentContactId == Id);

            if (getCascadeContact is null)
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

    }
}
