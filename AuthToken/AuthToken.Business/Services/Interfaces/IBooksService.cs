using AuthToken.ViewModels.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthToken.Business.Services.Interfaces
{
    public interface IBooksService
    {
        void Add();
        List<GetBooksView> GetList();
        GetByIdBooksView GetById(int bookId);
    }
}
