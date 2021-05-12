using RestWithAspNet5.Model;
using RestWithAspNet5.Repository;
using RestWithAspNet5.Repository.Generic;
using System.Collections.Generic;

namespace RestWithAspNet5.Business.Implementation
{
    public class BooksBusinessImplementation : IBooksBusiness
    {
        private readonly IRepository<Book> _repository;

        public BooksBusinessImplementation(IRepository<Book> repository)
        {
            _repository = repository;
        }


        public Book Create(Book book)
        {
            return _repository.Create(book);
        }

        public List<Book> FindAll()
        {
            return _repository.FindAll();
        }


        public Book FindById(long id)
        {
            return _repository.FindById(id);

        }

        public Book Update(Book book)
        {

            return _repository.Update(book);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

    }
}
