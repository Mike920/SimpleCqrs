using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MessageApp.Domain.Entities
{
    public class Message
    {
        protected Message()
        {
        }
        public Message(int id, string content, int receiverId, int senderId)
        {
            Id = id;
            Content = content;
            ReceiverId = receiverId;
            SenderId = senderId;
            SendDate = DateTime.Now;
        }

        public int Id { get; private set; }
        [Required]
        [MaxLength(1000)]
        public string Content { get; private set; }
        public DateTime SendDate { get; private set; }
        public DateTime? ReadDate { get; private set; }

        public int ReceiverId { get; private set; }
        [ForeignKey(nameof(ReceiverId))]
        public virtual Contact Receiver { get; private set; }
        
        public int SenderId { get; private set; }
        [ForeignKey(nameof(SenderId))]
        public virtual Contact Sender { get; private set; }
    }
}
