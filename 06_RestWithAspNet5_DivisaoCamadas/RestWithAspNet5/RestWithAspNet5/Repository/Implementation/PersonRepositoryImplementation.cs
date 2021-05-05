﻿using RestWithAspNet5.Model;
using RestWithAspNet5.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestWithAspNet5.Repository.Implementation
{
    public class PersonRepositoryImplementation : IPersonRepository
    {
        private MySqlContext _context;

        public PersonRepositoryImplementation(MySqlContext mySqlContext)
        {
            _context = mySqlContext;
        }


        public Person Create(Person person)
        {
            try
            {
                _context.Add<Person>(person);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }

            return person;
        }

        public List<Person> FindAll()
        {
            return _context.People.ToList();
        }


        public Person FindById(long id)
        {
            return _context.People.SingleOrDefault(p => p.Id.Equals(id));

        }

        public Person Update(Person person)
        {
            if (!Exists(person.Id)) return new Person();

            var result = _context.People.SingleOrDefault(p=> p.Id.Equals(person.Id));

            if (result != null)
            {
                try
                {
                    _context.Entry<Person>(result).CurrentValues.SetValues(person);
                    _context.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }

            }

            return person;
        }

        public void Delete(long id)
        {
            var result = _context.People.SingleOrDefault(p => p.Id.Equals(id));
            if (result != null)
            {
                try
                {
                    _context.People.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }

            }
        }


        public bool Exists(long id)
        {
            return _context.People.Any(p => p.Id.Equals(id));
        }


    }
}
