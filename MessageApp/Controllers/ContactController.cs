using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using MessageApp.Application.Contacts;
using MessageApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MessageApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : BaseApiController
    {
        private readonly IMediator _mediator;

        public ContactController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        // GET: api/<ContactController>
        [HttpGet]
        [ProducesResponseType(typeof(List<ContactDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetContactsQuery());
            return ApiContent(result);
        }

        // GET api/<ContactController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ContactDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _mediator.Send(new GetContactDetailsQuery(id));
            return ApiContent(result);
        }

        // POST api/<ContactController>
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType( StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Post([FromBody] Contact data)
        {
            var result = await _mediator.Send(new CreateContactCommand(data.Name));
            return ApiContent(result);
        }

        // PUT api/<ContactController>/5
        [HttpPut("{contactId}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put([FromRoute] int contactId, [FromBody] Contact data)
        {
            var result = await _mediator.Send(new UpdateContactCommand(contactId, data.Name));
            return ApiContent(result);
        }

        // DELETE api/<ContactController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteContactCommand(id));
            return ApiContent(result);
        }
    }
}
