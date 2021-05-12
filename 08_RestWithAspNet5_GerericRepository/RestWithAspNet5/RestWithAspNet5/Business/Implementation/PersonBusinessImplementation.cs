using RestWithAspNet5.Model;
using RestWithAspNet5.Repository;
using RestWithAspNet5.Repository.Generic;
using System.Collections.Generic;

namespace RestWithAspNet5.Business.Implementation
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
        private readonly IRepository<Person> _repository;

        public PersonBusinessImplementation(IRepository<Person> repository)
        {
            _repository = repository;
        }


        public Person Create(Person person)
        {
            return _repository.Create(person);
        }

        public List<Person> FindAll()
        {
            return _repository.FindAll();
        }


        public Person FindById(long id)
        {
            return _repository.FindById(id);

        }

        public Person Update(Person person)
        {

            return _repository.Update(person);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }


    }
}
