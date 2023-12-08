using Contacts.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult> GetContactTree(Guid id)
        {
            var getContactTree = await _contactRepo.GetContactTreeAsync(id);

            if (getContactTree.Item1 is null
                && getContactTree.Item2 is null)
                return BadRequest("contact not found");

            return Ok(getContactTree);
        }
    }
}
