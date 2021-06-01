using RestWithAspNet5.Data.Converter.Contract;
using RestWithAspNet5.Data.VO;
using RestWithAspNet5.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithAspNet5.Data.Converter.Implementations
{
    public class BookConverter : IParser<Book, BookVO>, IParser<BookVO, Book>
    {
        public BookVO Parse(Book origin)
        {
            if (origin ==null)
            {
                return null;
            }

            return new BookVO()
            {
                Id = origin.Id,
                Author = origin.Author,
                LaunchDate = origin.LaunchDate,
                Price = origin.Price,
                Title = origin.Title
            };
        }

        public List<BookVO> Parse(List<Book> origins)
        {
            if (origins == null)
            {
                return null;
            }

            return origins.Select(item => Parse(item)).ToList();
        }

        public Book Parse(BookVO origin)
        {
            if (origin == null)
            {
                return null;
            }

            return new Book()
            {
                Id = origin.Id,
                Author = origin.Author,
                LaunchDate = origin.LaunchDate,
                Price = origin.Price,
                Title = origin.Title
            };
        }

        public List<Book> Parse(List<BookVO> origins)
        {
            if (origins == null)
            {
                return null;
            }

            return origins.Select(item => Parse(item)).ToList();
        }
    }
}
