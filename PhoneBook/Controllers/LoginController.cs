using Contacts.Security.Contracts;
using Contacts.Security.Models;
using Microsoft.AspNetCore.Mvc;

namespace PhoneBook.API.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly IAuthenticationService _authService;

        public LoginController(IAuthenticationService _authService)
            => this._authService = _authService;

        [HttpPost]
        [ProducesResponseType(401)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<AuthResponse>> LogInAsync(AuthRequest req)
        {
            var response = await _authService.AuthenticateAsync(req);

            if (!response.status.Succeeded)
                return Unauthorized("Failed to log in, check login and password");

            return Ok(response);
        }
    }
}
