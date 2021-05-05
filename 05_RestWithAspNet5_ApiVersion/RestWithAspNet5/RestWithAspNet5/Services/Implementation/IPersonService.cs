using RestWithAspNet5.Model;
using System.Collections.Generic;

namespace RestWithAspNet5.Services.Implementation
{
    public interface IPersonService
    {
        Person Create(Person person);
        Person FindById(long id);
        List<Person> FindAll();
        Person Update(Person person);
        void Delete(long id);
    }
}
