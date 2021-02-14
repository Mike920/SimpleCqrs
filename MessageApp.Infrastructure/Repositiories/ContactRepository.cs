using MessageApp.Domain.Entities;
using MessageApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MessageApp.Infrastructure.Repositiories
{
    public class ContactRepository : IContactRepository
    {
        public async Task<IEnumerable<Contact>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
