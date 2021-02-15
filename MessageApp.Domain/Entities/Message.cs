using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MessageApp.Domain.Entities
{
    public class Message
    {
        protected Message()
        {
        }
        public Message(int id, string content, int contactId)
        {
            Id = id;
            Content = content;
            ContactId = contactId;
        }

        public int Id { get; private set; }
        [Required]
        [MaxLength(50)]
        public string Content { get; private set; }

        public int ContactId { get; private set; }
        public virtual Contact Contact { get; private set; }
    }
}
