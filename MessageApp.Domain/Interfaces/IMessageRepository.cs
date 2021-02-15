using MessageApp.Domain.Entities;
using MessageApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MessageApp.Domain.Interfaces
{
    public interface IMessageRepository
    {
        Task<IEnumerable<Message>> GetAll();
        Task<Message> Get(int id);
        Task<int> Create(Message message);
        Task Update(Message message);
        Task Delete(int id);
        Task<PaginatedList<Message>> Query(int? contactId, string content, int pageNumber, int pageSize, string sortColumn, bool sortDescending, int? senderId, bool? isRead);
    }
}
