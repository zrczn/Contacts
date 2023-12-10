using Contacts.Domain.DbEntites;
using Contacts.Domain.DTO;
using Contacts.Domain.IRepositories;
using Contacts.Domain.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
        private readonly IPersonRepository _personRepo;

        public PersonController(IPersonRepository personRepository)
            => _personRepo = personRepository;

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public async Task<ActionResult<IEnumerable<PersonDisplay>>> GetPersonsOverall()
        {
            var getPpls = await _personRepo.GetPersonOverallAsync();

            if (getPpls.Count() <= 0)
                return NoContent();

            return Ok(getPpls);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Authorize]
        public async Task<ActionResult<Person>> GetPerson(Guid id)
        {
            var getDetailedPerson = await _personRepo.GetPersonAsync(id);

            if (getDetailedPerson == null)
                return NotFound();

            return Ok(getDetailedPerson);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<IActionResult> DeletePerson(Guid id)
        {
            var deletePerson = await _personRepo.DeletePersonAsync(id);

            if (deletePerson)
                return Ok("Person has been deleted");

            return BadRequest("there's no such user");
        }

        //Create - Update
        [HttpPut("modify/")]
        [ProducesResponseType(400)]
        [ProducesResponseType(201)]
        [ProducesResponseType(204)]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Authorize]
        public async Task<IActionResult> CrupDatePerson(Guid id, PersonDTO personDTO)
        {
            string msg;

            var crupdate = await _personRepo.CrupDatePersonAsync(id, personDTO);

            msg = crupdate switch
            {
                0 => "invalid contacts passed",
                1 => "New Person Has Been Created",
                2 => "Person untouched",
                3 => "Person updated",
                4 => "Email in use",
                _ => "unknown"
            };

            if (crupdate == 0)
                return StatusCode(400, msg);

            if (crupdate == 1)
                return StatusCode(201, msg);

            if (crupdate == 2)
                return StatusCode(204, msg);

            if (crupdate == 3)
                return StatusCode(200, msg);

            if (crupdate == 4)
                return StatusCode(204, msg);


            return StatusCode(500, msg);
        }

    }
}
