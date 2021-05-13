using RestWithAspNet5.Data.Converter.Implementations;
using RestWithAspNet5.Data.VO;
using RestWithAspNet5.Model;
using RestWithAspNet5.Repository.Generic;
using System.Collections.Generic;

namespace RestWithAspNet5.Business.Implementation
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
        private readonly IRepository<Person> _repository;
        private readonly PersonConverter _personConverter;

        public PersonBusinessImplementation(IRepository<Person> repository)
        {
            _repository = repository;
            _personConverter = new PersonConverter();
        }

        public PersonVO FindById(long id)
        {
            return _personConverter.Parse(_repository.FindById(id));

        }

        public List<PersonVO> FindAll()
        {
            return _personConverter.Parse(_repository.FindAll());
        }

        public PersonVO Create(PersonVO personVO)
        {
            var personEntity = _personConverter.Parse(personVO);
            personEntity = _repository.Create(personEntity);

            return _personConverter.Parse(personEntity);
        }

        public PersonVO Update(PersonVO personVO)
        {

            var personEntity = _personConverter.Parse(personVO);
            personEntity = _repository.Update(personEntity);

            return _personConverter.Parse(personEntity);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }


    }
}
