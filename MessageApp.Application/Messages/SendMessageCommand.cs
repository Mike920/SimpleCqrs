using MediatR;
using MessageApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using MessageApp.Domain.Entities;
using MessageApp.Application.Models;
using FluentValidation;
using FluentValidation.Results;

namespace MessageApp.Application.Messages
{
    public class SendMessageCommand : IRequest<Result<int?>>
    {
        public SendMessageCommand(string content, int receiverId)
        {
            Content = content;
            ReceiverId = receiverId;
        }

        public string Content { get; }
        public int ReceiverId { get; }

        public ValidationResult Validate() => new Validator().Validate(this);

        public class Validator : AbstractValidator<SendMessageCommand>
        {
            public Validator()
            {
                RuleFor(x => x.Content).NotEmpty().MaximumLength(1000);
                RuleFor(x => x.ReceiverId).NotEmpty().GreaterThan(0);
            }
        }
    }

    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, Result<int?>>
    {
        private readonly IMessageRepository _messageRepository;
        private readonly ICurrentUser _currentUser;

        public SendMessageCommandHandler(IMessageRepository messageRepository, ICurrentUser currentUser)
        {
            this._messageRepository = messageRepository;
            this._currentUser = currentUser;
        }
        public async Task<Result<int?>> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            var validationResult = request.Validate();
            if (!validationResult.IsValid)
                return Result.UnprocessableEntity<int?>(null, validationResult.ToString());

            var message = new Message(0, request.Content, request.ReceiverId, _currentUser.UserId);
            var messageId = await _messageRepository.Create(message);

            return Result.Ok((int?)messageId);
        }
    }
}
