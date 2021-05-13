using RestWithAspNet5.Data.VO;
using RestWithAspNet5.Model;
using System.Collections.Generic;

namespace RestWithAspNet5.Business
{
    public interface IBooksBusiness
    {
        BookVO Create(BookVO bookVO);
        BookVO FindById(long id);
        List<BookVO> FindAll();
        BookVO Update(BookVO bookVO);
        void Delete(long id);
    }
}
