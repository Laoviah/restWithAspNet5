using RestWithAspNet5.Model;
using RestWithAspNet5.Repository.Generic;
using System.Collections.Generic;

namespace RestWithAspNet5.Repository
{
    public interface IPersonRepository : IRepository<Person>
    {
        Person Disabled(long id);
        List<Person> FindByName(string firstName, string lastName);

    }
}
