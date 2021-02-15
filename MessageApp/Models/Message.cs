using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MessageApp.Models
{
    public class Message
    {
        [Required]
        [MaxLength(1000)]
        public string Content { get; set; }
        [Required]
        public int ReceiverId { get; set; }
    }
}
