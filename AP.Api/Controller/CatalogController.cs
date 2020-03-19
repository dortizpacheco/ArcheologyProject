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
    [Route("/catalog")]
    [ApiController]
    public class CatalogController: ControllerBase
    {
        private ICatalogService _service;
        private IUserAuthenticationService _authentication;
        public CatalogController(ICatalogService service , IUserAuthenticationService authentication)
        {
            _service = service;
            _authentication = authentication;
            
        }
        
        [HttpGet]
        public async Task<ActionResult<ICollection<CatalogSummaryDto>>> GetAll([FromQuery] string fatherId)
        {   
            var result = await _service.GetAll(fatherId);

            if(result == null) return NotFound();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CatalogDetailsDto>> GetByIdAsync(string id)
        {
            var result = await _service.GetByIdAsync(id);

            if(result == null) return NotFound();

            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<CatalogDetailsDto>> CreateAsync([FromBody] CatalogNewDto newItem,string authentication)
        {
            bool check = await _authentication.IsLoginAsync(authentication);
            if(check)
            {
                var result = await _service.CreateAsync(newItem);

                if(result == null) return NotFound();

                return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Id }, result);;
            }
            return BadRequest("Not Loggin");
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(string id, string authentication)
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
        [HttpPut("{id}")]
        public async Task<ActionResult<CatalogDetailsDto>> UpdateByIdAsync(string id,[FromBody] CatalogNewDto newItem , string authentication)
        {
            bool check = await _authentication.IsLoginAsync(authentication);
            if(check)
            {
                var result = await _service.UpdateByIdAsync(id,newItem);

                if(result == null) return NotFound();

                return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Id }, result);;
            }
            return BadRequest("Not Loggin");
        }
    }
}