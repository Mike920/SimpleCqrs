﻿using MediatR;
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
    public class MarkMessageAsReadCommand : IRequest<Result<object>>
    {
        public MarkMessageAsReadCommand(int id)
        {
            Id = id;
        }

        public int Id { get; }

        public ValidationResult Validate() => new Validator().Validate(this);        

        public class Validator : AbstractValidator<MarkMessageAsReadCommand>
        {
            public Validator()
            {
                RuleFor(x => x.Id).GreaterThan(0);
            }
        }
    }

    public class MarkMessageAsReadCommandHandler : IRequestHandler<MarkMessageAsReadCommand,Result<object>>
    {
        private readonly IMessageRepository _messageRepository;

        public MarkMessageAsReadCommandHandler(IMessageRepository messageRepository)
        {
            this._messageRepository = messageRepository;
        }
        public async Task<Result<object>> Handle(MarkMessageAsReadCommand request, CancellationToken cancellationToken)
        {
            var validationResult = request.Validate();
            if (!validationResult.IsValid)
                return Result.UnprocessableEntity<object>(null, validationResult.ToString());

            var message = await _messageRepository.Get(request.Id);
            if (message == null)
                return Result.NotFound<object>(null);

            message.MarkAsRead();
            await _messageRepository.Update(message);

            return Result.NoContent<object>(null);
        }       
    }
}
