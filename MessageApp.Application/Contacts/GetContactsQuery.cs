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
    public class GetContactsQuery : IRequest<Result<List<ContactDto>>>
    {
    }

    public class GetContactsQueryHandler : IRequestHandler<GetContactsQuery, Result<List<ContactDto>>>
    {
        private readonly IContactRepository _contactRepository;

        public GetContactsQueryHandler(IContactRepository contactRepository)
        {
            this._contactRepository = contactRepository;
        }
        public async Task<Result<List<ContactDto>>> Handle(GetContactsQuery request, CancellationToken cancellationToken)
        {
            var contacts = (await _contactRepository.GetAll()).Select(x => new ContactDto(x.Id, x.Name)).ToList();
            
            if (contacts.Count == 0)
                return Result.NoContent(contacts);
            return Result.Ok(contacts);
        }
    }
}
