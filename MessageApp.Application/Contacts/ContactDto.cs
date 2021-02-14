using MessageApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageApp.Application.Contacts
{
    public class ContactDto
    {
        public ContactDto(Contact contact)
        {
            Id = contact.Id;
            Name = contact.Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
