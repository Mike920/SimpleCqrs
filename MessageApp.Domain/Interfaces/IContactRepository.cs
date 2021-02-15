using MessageApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MessageApp.Domain.Interfaces
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contact>> GetAll();
        Task<Contact> Get(int id);
        Task<int> Create(Contact contact);
        Task Update(Contact contact);
        Task Delete(int id);
    }
}
