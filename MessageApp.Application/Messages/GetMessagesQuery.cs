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
using FluentValidation.Results;
using FluentValidation;
using MessageApp.Domain.Entities;

namespace MessageApp.Application.Messages
{
    public class GetMessagesQuery : IRequest<Result<PaginatedList<MessageDto>>>
    {
        public GetMessagesQuery(int? receiverId, string content, int pageNumber, int pageSize, string sortColumn, bool sortDescending, int? senderId, DateTime? sendDate, DateTime? readDate)
        {
            ReceiverId = receiverId;
            Content = content;
            PageNumber = pageNumber;
            PageSize = pageSize;
            SortColumn = sortColumn;
            SortDescending = sortDescending;
            SenderId = senderId;
            SendDate = sendDate;
            ReadDate = readDate;
        }

        public int? ReceiverId { get; }
        public string Content { get; }
        public int PageNumber { get; }
        public int PageSize { get; }
        public string SortColumn { get; }
        public bool SortDescending { get; }
        public int? SenderId { get; }
        public DateTime? SendDate { get; }
        public DateTime? ReadDate { get; }

        public ValidationResult Validate() => new Validator().Validate(this);

        public class Validator : AbstractValidator<GetMessagesQuery>
        {
            public Validator()
            {
                var sortableColumns = new string[] { nameof(Message.Id), nameof(Message.Content), nameof(Message.SendDate), nameof(Message.ReadDate) };

                RuleFor(x => x.PageNumber).GreaterThan(0);
                RuleFor(x => x.PageSize).GreaterThan(0);
                RuleFor(x => x.SortColumn).Must(x => sortableColumns.Contains(x))
                    .WithMessage("'{PropertyName}' Only the following columns support sorting: " + string.Join(", ", sortableColumns));
            }
        }
    }

    public class GetMessagesQueryHandler : IRequestHandler<GetMessagesQuery, Result<PaginatedList<MessageDto>>>
    {
        private readonly IMessageRepository _messageRepository;
        private readonly ICurrentUser _currentUser;

        public GetMessagesQueryHandler(IMessageRepository messageRepository, ICurrentUser currentUser)
        {
            this._messageRepository = messageRepository;
            this._currentUser = currentUser;
        }
        public async Task<Result<PaginatedList<MessageDto>>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
        {
            var validationResult = request.Validate();
            if (!validationResult.IsValid)
                return Result.UnprocessableEntity<PaginatedList<MessageDto>>(null, validationResult.ToString());

            var queryResult = (await _messageRepository.Query(request.ReceiverId, request.Content, request.PageNumber, request.PageSize, request.SortColumn,
                request.SortDescending, request.SenderId, request.SendDate, request.ReadDate));

            var messages = queryResult.Items.Select(x => new MessageDto(x.Id, x.Content, x.SendDate, x.ReadDate, x.Sender, x.Receiver)).ToList();
            var result = new PaginatedList<MessageDto>(messages, queryResult.TotalCount, queryResult.PageNumber, queryResult.TotalPages);
            return Result.Ok(result);
        }
    }
}
