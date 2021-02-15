using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MessageApp.Domain.Entities
{
    public class Contact
    {
        protected Contact()
        {
        }
        public Contact(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; private set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; private set; }
    }
}
