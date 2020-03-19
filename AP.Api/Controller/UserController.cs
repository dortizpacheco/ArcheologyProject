using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AP.Core.Dtos;
using AP.Core.Services;
using System.Collections.Generic;
using System.Collections;
using System.Runtime;
using System;

namespace AP.Api.Controller
{
    [Route("/user")]
    [ApiController]
    public class UserController: ControllerBase
    {
        private IUserService _service;
        private IUserAuthenticationService _authentication;
        public UserController(IUserService service , IUserAuthenticationService authentication)
        {
            _service = service;
            _authentication = authentication;
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetByIdAsync(string id)
        {
            var result = await _service.GetByIdAsync(id);

            if(result == null) return NotFound();

            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateAsync([FromBody] NewUser newItem)
        {
            var result = await _service.CreateAsync(newItem);

            if(result == null) return NotFound();

            return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Id }, result);;
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(string id , string authentication)
        {
            bool check = await _authentication.IsLoginAsync(authentication);
            if(check)
            {
                var result = await _service.DeleteAsync(id);

                if(!result) return NotFound();

                return NoContent();
            }
            return BadRequest("Not Loggin");

        }
    }
}