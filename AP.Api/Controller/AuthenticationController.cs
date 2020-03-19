using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AP.Core.Dtos;
using AP.Core.Services;
using System;

namespace AP.Api.Controllers
{
    [Route("/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserAuthenticationService _service;

        public AuthenticationController(IUserAuthenticationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<string>> SignIn([FromQuery] string username, [FromQuery] string password)
        {       
            var signedInUser = await _service.LoginAsync(username, password);

            if (signedInUser == null)
                return NotFound();

            return signedInUser;
        }
    }
}
