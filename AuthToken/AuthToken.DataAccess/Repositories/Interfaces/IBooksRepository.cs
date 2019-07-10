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
        void Save();
    }
}
