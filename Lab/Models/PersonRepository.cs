using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace Lab.Models
{
    public class PersonRepository
    {
        private readonly ApplicationDbContext _context;

        public PersonRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Person> PersonList { get; set; }


        public PersonRepository()
        {
            PersonList = new List<Person>();

            Person p = new Person
            {
                PersonID = 100,
                FirstName = "Jimmy",
                LastName = "Johns",
                BirthDate = new DateTime(1990, 02, 23)
            };

            PersonList.Add(p);

            p = new Person
            {
                PersonID = 75,
                FirstName = "Arnold",
                LastName = "Schwarzenegger",
                BirthDate = new DateTime(1947, 07, 30)
            };

            PersonList.Add(p);

            p = new Person
            {
                PersonID = 34,
                FirstName = "Joey",
                LastName = "Tomatoes",
                BirthDate = new DateTime(1988, 12, 22)
            };

            PersonList.Add(p);

        }

        public void Add(Person person)
        {
            //PersonList.Add(person);
            _context.Add(person);
            _context.SaveChanges();
        }

       
        public void Remove(Person person)
        {
            _context.Remove(person);
            _context.SaveChanges();
        }

        public void Edit(Person person)
        {
            _context.Update(person);
            _context.SaveChanges();
        }
    }


}
