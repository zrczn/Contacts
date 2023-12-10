using Contacts.Domain.DbEntites;
using Contacts.Domain.DTO;
using Contacts.Domain.IRepositories;
using Contacts.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Persistence.Repositories
{
    internal class PersonRepository : AbstractRepository, IPersonRepository
    {
        private readonly IContactRepository _contactRepo;

        public PersonRepository(ContactsDatabaseContext _DbCon, IContactRepository contactRepository) : base(_DbCon)
        {
            _contactRepo = contactRepository;
        }

        //w zależności od podanego id:
        //jeżeli nie istnieje stwórz nową personę wdłg PersonDTO
        //jeżeli istnieje aktualizuj o PersonDTO

        public async Task<int> CrupDatePersonAsync(Guid Id, PersonDTO person)
        {
            //0 - invalid contacts passed
            //1 - created
            //2 - untouched
            //3 - updated
            //4 - email in use

            var findEmail = await _DbCon.Persons.AnyAsync(x => x.Email == person.Email);

            if (findEmail)
                return 4;

            var findParentContact = await _DbCon.Contacts.FirstOrDefaultAsync(x => x.ParentContactId == person.ParentContactId);

            if (findParentContact is null)
                return 0;


            Person getPerson = await _DbCon.Persons.FirstOrDefaultAsync(x => x.Id == Id);

            (ContactDTO, IEnumerable<ContactDTO>) getCascade = await _contactRepo.GetContactTreeAsync(person.ParentContactId);

            if (getCascade.Item1 == null && getCascade.Item2.Count() <= 0)
                return 0;

            IEnumerable<Guid> getIdsOfLeaves = getCascade.Item2.Select(x => x.Id);
            Contact getCorrespondingContact = await _DbCon.Contacts.FirstOrDefaultAsync(x => x.ParentContactId == getCascade.Item1.Id
                                                    && x.Id == person.SubContactId);

            if (getPerson is null)
            {
                Person p;

                if (getCascade.Item1 != null && getCascade.Item2.Count() <= 0)
                {
                    p = PersonCreator(person, getCascade.Item1.Id);

                    _DbCon.Add(p);
                    await _DbCon.SaveChangesAsync();
                    return 1;
                }

                if (getIdsOfLeaves.Contains(person.SubContactId))
                {
                    p = PersonCreator(person, getCorrespondingContact.Id);

                    _DbCon.Add(p);
                    await _DbCon.SaveChangesAsync();
                    return 1;
                }

                return 0;
            }
            else
            {
                if (getPerson.Id == person.Id &&
                    getPerson.FirstName == person.FirstName &&
                    getPerson.LastName == person.LastName &&
                    getPerson.Email == person.Email &&
                    getPerson.Password == person.Password &&
                    getPerson.PhoneNumber == person.PhoneNumber &&
                    getPerson.DateOfBirth == person.DateOfBirth &&
                    getPerson.ContactId == person.SubContactId)
                {
                    return 2;
                }

                getPerson.FirstName = person.FirstName;
                getPerson.LastName = person.LastName;
                getPerson.Email = person.Email;
                getPerson.Password = person.Password;
                getPerson.PhoneNumber = person.PhoneNumber;
                getPerson.DateOfBirth = person.DateOfBirth;

                if (getCascade.Item1 != null && getCascade.Item2.Count() <= 0)
                {
                    getPerson.ContactId = getCascade.Item1.Id;
                    return 3;
                }
                else if (getIdsOfLeaves.Contains(person.SubContactId))
                {
                    getPerson.ContactId = getCorrespondingContact.Id;
                    return 3;
                }

                return 0;
            }
        }

        private Person PersonCreator(PersonDTO dtoObj, Guid contactId)
        {
            Person person = new()
            {
                FirstName = dtoObj.FirstName,
                LastName = dtoObj.LastName,
                Email = dtoObj.Email,
                Password = dtoObj.Password,
                PhoneNumber = dtoObj.PhoneNumber,
                DateOfBirth = dtoObj.DateOfBirth,

                ContactId = contactId
            };

            return person;
        }

        public async Task<bool> DeletePersonAsync(Guid Id)
        {

            var getPrsn = await _DbCon.Persons.FirstOrDefaultAsync(x => x.Id == Id);

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

        //pobierz Person z przypisanym kontaktem i sub kontaktem
        public async Task<PersonDTO> GetPersonAsync(Guid Id)
        {
            var getPerson = await _DbCon.Persons.Include(y => y.Contact).ThenInclude(z => z.ContactRel).FirstOrDefaultAsync(x => x.Id == Id);

            if (getPerson is null)
                return null;

            var intoPersonDTO = new PersonDTO()
            {
                Id = getPerson.Id,
                FirstName = getPerson.FirstName,
                LastName = getPerson.LastName,
                Email = getPerson.Email,
                Password = getPerson.Password,
                PhoneNumber = getPerson.PhoneNumber,
                DateOfBirth = getPerson.DateOfBirth,

                SubContactId = getPerson.Contact.Id,
                SubContactName = getPerson.Contact.Name
            };

            try
            {
                intoPersonDTO.ParentContactId = (Guid)getPerson.Contact.ParentContactId;
                intoPersonDTO.ParentContactName = getPerson.Contact.ContactRel.Name;
            }
            catch
            {
                intoPersonDTO.ParentContactId = getPerson.ContactId;
                intoPersonDTO.ParentContactName = getPerson.Contact.Name;
                intoPersonDTO.SubContactId = Guid.Empty;
                intoPersonDTO.SubContactName = null;
            }

            return intoPersonDTO;
        }

        //pobierz Person z danymi ogólnymi, (dostępna dla każdego)

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
    }
}
