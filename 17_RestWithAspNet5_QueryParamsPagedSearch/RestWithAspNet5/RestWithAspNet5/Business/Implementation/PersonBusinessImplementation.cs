using RestWithAspNet5.Data.Converter.Implementations;
using RestWithAspNet5.Data.VO;
using RestWithAspNet5.Hypermedia.Utils;
using RestWithAspNet5.Model;
using RestWithAspNet5.Repository;
using System.Collections.Generic;

namespace RestWithAspNet5.Business.Implementation
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
        private readonly IPersonRepository _repository;
        private readonly PersonConverter _personConverter;

        public PersonBusinessImplementation(IPersonRepository repository)
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

        public PersonVO Disable(long id)
        {
            var personEntity = _repository.Disabled(id);

            return _personConverter.Parse(personEntity);
        }

        public List<PersonVO> FindByName(string firstName, string lastName)
        {
            return _personConverter.Parse(_repository.FindByName(firstName, lastName));
        }

        public PagedSearchVO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {
            var sort = (!string.IsNullOrWhiteSpace(sortDirection) && !sortDirection.Equals("desc")) ? "asc" : "desc";
            var size = (pageSize < 1) ? 10 : pageSize;
            var offset = page > 0 ? (page - 1) * size : 0;

            string query = @"select * from person p where 1 = 1 ";

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query + $" and p.first_name like '%{name}%' ";
            }

            query = query + $"order by p.first_name {sort} limit {size} offset {offset};";

            string countQuery = @"select count(*) from person p where 1 = 1 ";
            if (!string.IsNullOrWhiteSpace(name))
            {
                countQuery = countQuery + $" and p.first_name like '%{name}%' ; ";
            }
            var persons = _repository.FindWithPagedSearch(query);

            int totalResults = _repository.GetCount(countQuery);

            return new PagedSearchVO<PersonVO>() {
                CurrentPage = page,
                List = _personConverter.Parse(persons),
                SortDirections = sort, 
                TotalResults = totalResults

            };
        }
    }
}
