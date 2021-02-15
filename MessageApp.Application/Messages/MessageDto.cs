using MessageApp.Application.Contacts;
using MessageApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageApp.Application.Messages
{
    public class MessageDto
    {
        public MessageDto(int id, string content, DateTime sendDate, DateTime? readDate, Contact sender, Contact receiver)
        {
            Id = id;
            Content = content;
            SendDate = sendDate;
            ReadDate = readDate;
            Sender = new ContactDto(sender.Id, sender.Name);
            Receiver = new ContactDto(receiver.Id, receiver.Name);
        }

        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime SendDate { get; private set; }
        public DateTime? ReadDate { get; private set; }

        public virtual ContactDto Receiver { get; private set; }

        public virtual ContactDto Sender { get; private set; }
    }
}
