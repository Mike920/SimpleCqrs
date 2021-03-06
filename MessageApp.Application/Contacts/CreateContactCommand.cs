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

namespace MessageApp.Application.Contacts
{
    public class CreateContactCommand : IRequest<Result<int?>>
    {
        public CreateContactCommand(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public ValidationResult Validate() => new Validator().Validate(this);

        public class Validator : AbstractValidator<CreateContactCommand>
        {
            public Validator()
            {
                RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            }
        }
    }

    public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, Result<int?>>
    {
        private readonly IContactRepository _contactRepository;

        public CreateContactCommandHandler(IContactRepository contactRepository)
        {
            this._contactRepository = contactRepository;
        }
        public async Task<Result<int?>> Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {
            var validationResult = request.Validate();
            if (!validationResult.IsValid)
                return Result.UnprocessableEntity<int?>(null, validationResult.ToString());

            var contact = new Contact(0, request.Name);
            var contactId = await _contactRepository.Create(contact);

            return Result.Ok((int?)contactId);
        }
    }
}
