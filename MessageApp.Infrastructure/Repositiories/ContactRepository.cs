using MessageApp.Domain.Entities;
using MessageApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MessageApp.Infrastructure.Repositiories
{
    public class ContactRepository : IContactRepository
    {
        private readonly Context.AppContext _context;

        public ContactRepository(Context.AppContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<Contact>> GetAll()
        {
            return await _context.Contacts.AsNoTracking().ToListAsync();
        }
    }
}
