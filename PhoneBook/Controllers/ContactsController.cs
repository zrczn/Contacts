using Contacts.Domain.DbEntites;
using Contacts.Domain.DTO;
using Contacts.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PhoneBook.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ContactsController : Controller
    {
        private readonly IContactRepository _contactRepo;

        public ContactsController(IContactRepository contactRepository)
            => _contactRepo = contactRepository;

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public async Task<ActionResult<IEnumerable<PersonDisplay>>> GetPersonsOverall()
        {
            var getPpls = await _contactRepo.GetPersonOverallAsync();

            if (getPpls.Count() <= 0)
                return NoContent();

            return Ok(getPpls);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Person>> GetPerson(Guid id)
        {
            var getDetailedPerson = await _contactRepo.GetPersonAsync(id);

            if (getDetailedPerson == null)
                return NotFound();

            return Ok(getDetailedPerson);
        }
    }
}
