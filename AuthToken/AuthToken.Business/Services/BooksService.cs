using AuthToken.Business.Services.Interfaces;
using AuthToken.DataAccess.Entities;
using AuthToken.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthToken.Business.Services
{
    public class BooksService : IBooksService
    {
        private readonly IBooksRepository _booksRepository;

        public BooksService(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        public void AddBook()
        {
            var book = new Book();

            book.Name = "First Book";
            book.Price = 145.70M;

            _booksRepository.Create(book);
            _booksRepository.Save();
        }
    }
}
