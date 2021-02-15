using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MessageApp.Models
{
    public class Contact
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
