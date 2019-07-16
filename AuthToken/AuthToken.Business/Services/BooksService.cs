using AuthToken.Business.Services.Interfaces;
using AuthToken.DataAccess.Entities;
using AuthToken.DataAccess.Repositories.Interfaces;
using AuthToken.ViewModels.Books;
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


        public List<GetBooksView> GetList()
        {
            var books = _booksRepository.GetList();

            var view = new List<GetBooksView>();

            foreach (var book in books)
            {
                var bookView = new GetBooksView();
                bookView.Id = book.Id;
                bookView.Name = book.Name;
                bookView.Price = book.Price;

                view.Add(bookView);
            }

            return view;
        }


        public GetByIdBooksView GetById(int bookId)
        {
            var book = _booksRepository.Get(bookId);

            var view = new GetByIdBooksView();
            view.Id = book.Id;
            view.Name = book.Name;
            view.Price = book.Price;
            
            return view;
        }


        public void Add()
        {
            var book = new Book();

            book.Name = "First Book";
            book.Price = 145.70M;

            _booksRepository.Create(book);
            _booksRepository.Save();
        }
    }
}
