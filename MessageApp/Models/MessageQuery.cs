using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MessageApp.Models
{
    public class MessageQuery
    {
        [Range(1,int.MaxValue)]
        public int PageNumber { get; set; } = 1;
        [Range(1, int.MaxValue)]
        public int PageSize { get; set; } = 10;
        public string SortColumn { get; set; }

        public string Content { get; set; }
        public int? ReceiverId { get; set; }
        public int? SenderId { get; set; }
        public bool SortDescending { get; set; }
        public bool? IsRead { get; set; }
    }
}
