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

namespace MessageApp.Application.Contacts
{
    public class UpdateContactCommand : IRequest<Result<object>>
    {
        public UpdateContactCommand(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public int Id { get; set; }
        public string Name { get; }
    }

    public class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand,Result<object>>
    {
        private readonly IContactRepository _contactRepository;

        public UpdateContactCommandHandler(IContactRepository contactRepository)
        {
            this._contactRepository = contactRepository;
        }
        public async Task<Result<object>> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
        {
            var contact = new Contact(request.Id,request.Name);
            await _contactRepository.Update(contact);

            return Result.NoContent<object>(null);
        }
    }
}
