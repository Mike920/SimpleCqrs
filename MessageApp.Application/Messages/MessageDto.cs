using MessageApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageApp.Application.Messages
{
    public class MessageDto
    {
        public MessageDto(int id, string content, int contactId)
        {
            Id = id;
            Content = content;
            ContactId = contactId;
        }

        public int Id { get; set; }
        public string Content { get; set; }
        public int ContactId { get; }
    }
}
