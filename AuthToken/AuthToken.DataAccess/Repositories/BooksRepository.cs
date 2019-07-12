using AuthToken.DataAccess.Connections;
using AuthToken.DataAccess.Entities;
using AuthToken.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthToken.DataAccess.Repositories
{
    public class BooksRepository : IBooksRepository
    {
        private ApplicationDbConnection _context;

        public BooksRepository(ApplicationDbConnection context)
        {
            _context = context;
        }

        public IEnumerable<Book> GetBookList()
        {
            return _context.Books;
        }

        public Book GetBook(int id)
        {
            return _context.Books.Find(id);
        }

        public void Create(Book book)
        {
            _context.Books.Add(book);
        }

        public void Update(Book book)
        {
            _context.Entry(book).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Book book = _context.Books.Find(id);
            if (book != null)
                _context.Books.Remove(book);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
