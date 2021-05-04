using RestWithAspNet5.Model;
using RestWithAspNet5.Services.Implementation;
using System;
using System.Collections.Generic;

namespace RestWithAspNet5.Services
{
    public class PersonServiceImplementation : IPersonService
    {
       

        public Person Create(Person person)
        {
            return person;
        }

        public void Delete(long id)
        {
            
        }

        public List<Person> FindAll()
        {
            List<Person> people = new List<Person>();
            for(int i = 0; i < 8; i++)
            {
                Person person = MockPerson(i);
                people.Add(person);
            }

            return people;
        }


        public Person FindById(long id)
        {
            return new Person {
                Id = 1,
                FirstName = "Vially",
                LastName = "Israel",
                Address = "Rua 1234",
                Gender = "M"
            };
        }

        public Person Update(Person person)
        {
            return person;
        }


        private Person MockPerson(int id)
        {
            return new Person
            {
                Id = id+1,
                FirstName = (id+1).ToString() +" Vially",
                LastName = "Israel",
                Address = "Rua 1234",
                Gender = "M"
            };
        }


    }
}
