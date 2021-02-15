using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using MessageApp.Application.Contacts;
using MessageApp.Application.Messages;
using MessageApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MessageApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : BaseApiController
    {
        private readonly IMediator _mediator;

        public MessageController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        // GET: api/<MessageController>
        [HttpGet]
        [ProducesResponseType(typeof(List<MessageDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(MessageQuery data)
        {
            var result = await _mediator.Send(new GetMessagesQuery(data.ContactId, data.Content, data.PageNumber, data.PageSize, data.SortColumn, data.SortDescending));
            return ApiContent(result);
        }

        //// GET api/<MessageController>/5
        //[HttpGet("{id}")]
        //[ProducesResponseType(typeof(MessageDto), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> Get(int id)
        //{
        //    var result = await _mediator.Send(new GetMessageDetailsQuery(id));
        //    return ApiContent(result);
        //}

        //// POST api/<MessageController>
        //[HttpPost]
        //[ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        //[ProducesResponseType( StatusCodes.Status422UnprocessableEntity)]
        //public async Task<IActionResult> Post([FromBody] Message data)
        //{
        //    var result = await _mediator.Send(new CreateMessageCommand(data.Name));
        //    return ApiContent(result);
        //}

        //// PUT api/<MessageController>/5
        //[HttpPut("{messageId}")]
        //[ProducesResponseType(typeof(int), StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> Put([FromRoute] int messageId, [FromBody] Message data)
        //{
        //    var result = await _mediator.Send(new UpdateMessageCommand(messageId, data.Name));
        //    return ApiContent(result);
        //}

        //// DELETE api/<MessageController>/5
        //[HttpDelete("{id}")]
        //[ProducesResponseType(typeof(int), StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var result = await _mediator.Send(new DeleteMessageCommand(id));
        //    return ApiContent(result);
        //}
    }
}
