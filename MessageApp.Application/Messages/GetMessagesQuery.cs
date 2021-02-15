using MediatR;
using MessageApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using MessageApp.Application.Models;
using MessageApp.Domain.Models;

namespace MessageApp.Application.Messages
{
    public class GetMessagesQuery : IRequest<Result<List<MessageDto>>>
    {
        public GetMessagesQuery(int? contactId, string content, int pageNumber, int pageSize, string sortColumn, bool sortDescending)
        {
            ContactId = contactId;
            Content = content;
            PageNumber = pageNumber;
            PageSize = pageSize;
            SortColumn = sortColumn;
            SortDescending = sortDescending;
        }

        public int? ContactId { get; }
        public string Content { get; }
        public int PageNumber { get; }
        public int PageSize { get; }
        public string SortColumn { get; }
        public bool SortDescending { get; }
    }

    public class GetMessagesQueryHandler : IRequestHandler<GetMessagesQuery, Result<List<MessageDto>>>
    {
        private readonly IMessageRepository _messageRepository;

        public GetMessagesQueryHandler(IMessageRepository messageRepository)
        {
            this._messageRepository = messageRepository;
        }
        public async Task<Result<List<MessageDto>>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
        {
            var queryResult = (await _messageRepository.Query(request.ContactId, request.Content, request.PageNumber, request.PageSize, request.SortColumn, request.SortDescending));

            var messages = queryResult.Items.Select(x => new MessageDto(x.Id, x.Content, x.ContactId)).ToList();
            var result = new PaginatedList<MessageDto>(messages, queryResult.TotalCount, queryResult.PageNumber, queryResult.TotalPages);
            return Result.Ok(messages);
        }
    }
}
