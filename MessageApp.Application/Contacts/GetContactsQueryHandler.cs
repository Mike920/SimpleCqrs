using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MessageApp.Domain.Interfaces;
using MessageApp.Domain.Entities;
using System.Linq;
namespace MessageApp.Application.Contacts
{
    public class GetContactsQueryHandler : IRequestHandler<GetContactsQuery, List<ContactDto>>
    {
        private readonly IContactRepository _contactRepository;
         
        public GetContactsQueryHandler(IContactRepository contactRepository)
        {
            this._contactRepository = contactRepository;
        }
        public async Task<List<ContactDto>> Handle(GetContactsQuery request, CancellationToken cancellationToken)
        {
            var contacts = (await _contactRepository.GetAll()).Select(x => new ContactDto(x)).ToList();
            return contacts;
        }
    }
}
