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
    public class DeleteContactCommand : IRequest<Result<object>>
    {
        public DeleteContactCommand(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }

    public class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand, Result<object>>
    {
        private readonly IContactRepository _contactRepository;

        public DeleteContactCommandHandler(IContactRepository contactRepository)
        {
            this._contactRepository = contactRepository;
        }
        public async Task<Result<object>> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
        {
            await _contactRepository.Delete(request.Id);

            return Result.Ok<object>(null);
        }
    }
}
