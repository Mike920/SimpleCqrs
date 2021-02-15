using MessageApp.Domain.Entities;
using MessageApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MessageApp.Infrastructure.Extensions;
using MessageApp.Domain.Models;

namespace MessageApp.Infrastructure.Repositiories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly Context.EntityContext _context;

        public MessageRepository(Context.EntityContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<int> Create(Message message)
        {
            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
            return message.Id;
        }

        public async Task Delete(int id)
        {
            var message = await Get(id);
            _context.Messages.Remove(message);

            await _context.SaveChangesAsync();
        }

        public async Task<Message> Get(int id)
        {
            return await _context.Messages.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Message>> GetAll()
        {
            return await _context.Messages.AsNoTracking().ToListAsync();
        }

        public async Task<PaginatedList<Message>> Query(int? receiverId, string content, int pageNumber, int pageSize, string sortColumn, bool sortDescending, int? senderId, DateTime? sendDate, DateTime? readDate)
        {
            var source = _context.Messages
                .Include(x => x.Receiver)
                .Include(x => x.Receiver)
                .AsNoTracking();

            if (receiverId != null)
                source = source.Where(x => x.ReceiverId == receiverId);
            if (senderId != null)
                source = source.Where(x => x.SenderId == senderId);
            if (!string.IsNullOrWhiteSpace(content))
                source = source.Where(x => x.Content.Contains(content));
            if (sendDate != null)
                source = source.Where(x => x.SendDate == sendDate);
            if (readDate != null)
                source = source.Where(x => x.ReadDate == readDate);

            if (!string.IsNullOrWhiteSpace(sortColumn))
            {
                if(!sortDescending)
                    source = source.OrderBy(sortColumn);
                else
                    source = source.OrderByDescending(sortColumn);
            }

            return await source.PaginatedListAsync(pageNumber, pageSize);
        }

        public async Task Update(Message message)
        {
            _context.Update(message);
            await _context.SaveChangesAsync();
        }
    }
}
