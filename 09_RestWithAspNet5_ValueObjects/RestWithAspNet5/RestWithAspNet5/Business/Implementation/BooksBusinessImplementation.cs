using RestWithAspNet5.Data.Converter.Implementations;
using RestWithAspNet5.Data.VO;
using RestWithAspNet5.Model;
using RestWithAspNet5.Repository;
using RestWithAspNet5.Repository.Generic;
using System.Collections.Generic;

namespace RestWithAspNet5.Business.Implementation
{
    public class BooksBusinessImplementation : IBooksBusiness
    {
        private readonly IRepository<Book> _repository;
        private readonly BookConverter _bookConverter;

        public BooksBusinessImplementation(IRepository<Book> repository)
        {
            _repository = repository;
            _bookConverter = new BookConverter();

        }


        public BookVO Create(BookVO bookVO)
        {
            var bookEntity = _bookConverter.Parse(bookVO);
            bookEntity = _repository.Create(bookEntity);

            return _bookConverter.Parse(bookEntity);
        }


        public BookVO Update(BookVO bookVO)
        {

            var bookEntity = _bookConverter.Parse(bookVO);
            bookEntity = _repository.Update(bookEntity);

            return _bookConverter.Parse(bookEntity);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }
        public List<BookVO> FindAll()
        {
            return _bookConverter.Parse(_repository.FindAll());
        }


        public BookVO FindById(long id)
        {
            return _bookConverter.Parse(_repository.FindById(id));

        }

    }
}
