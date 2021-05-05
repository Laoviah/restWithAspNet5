using RestWithAspNet5.Model;
using RestWithAspNet5.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestWithAspNet5.Business.Implementation
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
        private readonly IPersonRepository _repository;

        public PersonBusinessImplementation(IPersonRepository repository)
        {
            _repository = repository;
        }


        public Person Create(Person person)
        {
            return _repository.Create(person);
        }

        public List<Person> FindAll()
        {
            return _repository.FindAll() ;
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
