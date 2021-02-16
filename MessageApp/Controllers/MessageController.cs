using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using MessageApp.Application.Contacts;
using MessageApp.Application.Messages;
using MessageApp.Domain.Models;
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
        [Route("query")]
        [HttpPost]
        [ProducesResponseType(typeof(PaginatedList<MessageDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Get(MessageQuery data)
        {
            var result = await _mediator.Send(new GetMessagesQuery(data.ReceiverId, data.Content, data.PageNumber, data.PageSize, 
                data.SortColumn, data.SortDescending, data.SenderId, data.IsRead));
            return ApiContent(result);
        }


        // POST api/<MessageController>
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Send([FromBody] Message data)
        {
            var result = await _mediator.Send(new SendMessageCommand(data.Content, data.ReceiverId));
            return ApiContent(result);
        }

        // PUT api/<MessageController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> MarkAsRead([FromRoute] int id)
        {
            var result = await _mediator.Send(new MarkMessageAsReadCommand(id));
            return ApiContent(result);
        }

        // DELETE api/<MessageController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteMessageCommand(id));
            return ApiContent(result);
        }
    }
}
