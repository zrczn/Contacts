using Contacts.Domain.DbEntites;
using Contacts.Domain.DTO;
using Contacts.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Reflection.Metadata.Ecma335;

namespace PhoneBook.API.Controllers
{
    [Route("person/")]
    [ApiController]
    public class PersonController : Controller
    {
        private readonly IContactRepository _contactRepo;

        public PersonController(IContactRepository contactRepository)
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

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeletePerson(Guid id)
        {
            var deletePerson = await _contactRepo.DeletePersonAsync(id);

            if (deletePerson)
                return Ok("Person has been deleted");

            return BadRequest("there's no such user");
        }

        //Create - Update
        [HttpPut("modify/")]
        [ProducesResponseType(201)]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CrupDatePerson(Guid id, PersonDTO personDTO)
        {
            string msg;

            var crupdate = await _contactRepo.CrupDatePersonAsync(id, personDTO);

            msg = crupdate switch
            {
                1 => "New Person Has Been Created",
                2 => "Person Has Beed Updated",
                _ => "unknwn error try again later"
            };

            if (crupdate == 1)
                return StatusCode(201, msg);

            if (crupdate == 2)
                return StatusCode(200, msg);
            
            return StatusCode(500, msg);

        }

    }
}
