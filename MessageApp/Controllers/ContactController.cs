using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using MessageApp.Application.Contacts;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MessageApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContactController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        // GET: api/<ContactController>
        [ProducesResponseType(typeof(List<ContactDto>), (int)HttpStatusCode.OK)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var contacts = await _mediator.Send(new GetContactsQuery());
            return Ok(contacts);
        }

        // GET api/<ContactController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ContactController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ContactController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ContactController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
