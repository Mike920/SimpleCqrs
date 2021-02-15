using MessageApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageApp.Application.Contacts
{
    public class ContactDto
    {
        public ContactDto(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
