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
        private readonly Context.EntityContext _context;

        public ContactRepository(Context.EntityContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<int> Create(Contact contact)
        {
            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();
            return contact.Id;
        }

        public async Task Delete(int id)
        {
            var contact = await Get(id);
            _context.Contacts.Remove(contact);

            await _context.SaveChangesAsync();
        }

        public async Task<Contact> Get(int id)
        {
            return await _context.Contacts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Contact>> GetAll()
        {
            return await _context.Contacts.AsNoTracking().ToListAsync();
        }

        public async Task Update(Contact contact)
        {
            _context.Update(contact);
            await _context.SaveChangesAsync();
        }
    }
}
