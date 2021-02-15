using MediatR;
using MessageApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using MessageApp.Application.Models;

namespace MessageApp.Application.Contacts
{
    public class GetContactDetailsQuery : IRequest<Result<ContactDto>>
    {
        public GetContactDetailsQuery(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }

    public class GetContactDetailsQueryHandler : IRequestHandler<GetContactDetailsQuery, Result<ContactDto>>
    {
        private readonly IContactRepository _contactRepository;

        public GetContactDetailsQueryHandler(IContactRepository contactRepository)
        {
            this._contactRepository = contactRepository;
        }
        public async Task<Result<ContactDto>> Handle(GetContactDetailsQuery request, CancellationToken cancellationToken)
        {
            var contact = await _contactRepository.Get(request.Id);
           
            if (contact == null)
                return Result.NotFound<ContactDto>(null);

            var contactDto = new ContactDto(contact.Id, contact.Name);
            return Result.Ok(contactDto);
        }
    }
}
