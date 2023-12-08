using Contacts.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Contacts.API.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactRepository _contactRepo;

        public ContactController(IContactRepository contactRepository)
                => _contactRepo = contactRepository;

        [HttpGet("tree/{id:guid}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        [Authorize]
        public async Task<ActionResult> GetContactTree(Guid id)
        {
            var getContactTree = await _contactRepo.GetContactTreeAsync(id);

            if (getContactTree.Item1 is null
                && getContactTree.Item2 is null)
                return BadRequest("contact not found");

            return Ok(getContactTree);
        }

        [HttpPost("{name}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        [ProducesResponseType(201)]
        [Authorize]

        public async Task<IActionResult> CreateContact(string name)
        {
            //0 - bad entry
            //1 - already exists
            //2 - internal error
            //3 - ok

            int createContact = await _contactRepo.CreateContact(name);

            switch (createContact)
            {
                case 0:
                    return BadRequest();
                case 1:
                    return Conflict();
                case 2:
                    return StatusCode(500);
                case 3:
                    return Created();
            }

            return StatusCode(500);
        }
    }
}
