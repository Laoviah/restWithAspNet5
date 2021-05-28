using RestWithAspNet5.Model;
using RestWithAspNet5.Model.Context;
using RestWithAspNet5.Repository.Generic;
using System;
using System.Linq;

namespace RestWithAspNet5.Repository
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {

        public PersonRepository(MySqlContext context) : base (context)
        {

        }

        public Person Disabled(long id)
        {
            var user = _context.People.SingleOrDefault(p => p.Id.Equals(id));
            if (user == null)
            {
                return null;
            }

            user.Enabled = false;
            try
            {
                _context.Entry(user).CurrentValues.SetValues(user);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }

            return user;
        }
    }
}
