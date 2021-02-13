using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageApp.Application.Contacts
{
    public class GetContactsQuery : IRequest<List<ContactDto>>
    {
    }
}
