using RestWithAspNet5.Model;
using RestWithAspNet5.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestWithAspNet5.Repository.Implementation
{
    public class BookRepositoryImplementation : IBookRepository
    {

        private MySqlContext _context;

        public BookRepositoryImplementation(MySqlContext mySqlContext)
        {
            _context = mySqlContext;
        }


        public Book Create(Book book)
        {
            try
            {
                _context.Add<Book>(book);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }

            return book;
        }

        public void Delete(long id)
        {
            var result = _context.Books.SingleOrDefault(p => p.Id.Equals(id));
            if (result != null)
            {
                try
                {
                    _context.Books.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }

            }
        }

        public bool Exists(long id)
        {
            return _context.Books.Any(p => p.Id.Equals(id));
        }

        public List<Book> FindAll()
        {
            return _context.Books.ToList();
        }

        public Book FindById(long id)
        {
            return _context.Books.SingleOrDefault(p => p.Id.Equals(id));
        }

        public Book Update(Book book)
        {
            if (!Exists(book.Id)) return null;

            var result = _context.People.SingleOrDefault(p => p.Id.Equals(book.Id));

            if (result != null)
            {
                try
                {
                    _context.Entry<Person>(result).CurrentValues.SetValues(book);
                    _context.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }

            }

            return book;
        }
    }
}
