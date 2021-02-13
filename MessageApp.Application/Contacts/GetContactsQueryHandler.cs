using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MessageApp.Domain.Interfaces;

namespace MessageApp.Application.Contacts
{
    public class GetContactsQueryHandler : IRequestHandler<GetContactsQuery, List<ContactDto>>
    {
        private readonly IContactRepository _contactRepository;

        public GetContactsQueryHandler(IContactRepository contactRepository)
        {
            this._contactRepository = contactRepository;
        }
        public Task<List<ContactDto>> Handle(GetContactsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
