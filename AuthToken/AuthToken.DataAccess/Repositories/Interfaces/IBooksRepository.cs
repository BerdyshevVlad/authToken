using AuthToken.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthToken.DataAccess.Repositories.Interfaces
{
    public interface IBooksRepository
    {
        void Create(Book book);
        List<Book> GetList();
        Book Get(int id);
        void Update(Book book);
        void Delete(int id);
        void Save();
    }
}
