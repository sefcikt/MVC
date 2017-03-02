using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lab.Models;

namespace Lab.Controllers
{
    public class HomeController : Controller
    {
        private PersonRepository repo;

        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
            repo = new PersonRepository(context);
        }

        public IActionResult Index()
        {
            DateTime dt = DateTime.Now;
            ViewData["Time"] = dt.ToString("hh:mm tt");
            ViewData["Day"] = dt.DayOfWeek.ToString();
            ViewData["Month"] = dt.ToString("MMMM");
            ViewData["Num"] = dt.Day.ToString();
            ViewData["Year"] = dt.Year.ToString();

            int daysInYear = DateTime.IsLeapYear(dt.Year) ? 366 : 365;
            ViewData["nextYear"] = daysInYear - dt.DayOfYear;

            var hour = dt.Hour;
            if (hour < 12)
            {
                ViewData["Greeting"] = "Good Morning!";
            }
            else if (hour < 18)
            {
                ViewData["Greeting"] = "Good Afternoon!";
            }
            else
            {
                ViewData["Greeting"] = "Good Evening!";
            }

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult ExamplePerson()
        {
            Person p = new Person
            {
                PersonID = 25,
                FirstName = "Johnny",
                LastName = "Bananas",
                BirthDate = new DateTime(1978, 06, 23)
            };

            return View(p);
        }
        
        public IActionResult ShowList()
        {
            return View(_context.Persons.ToList());
        }
        
        public IActionResult ShowPerson(int? id)
        {
            Person person;
            if (id == null)
            {
                person = new Person
                {
                    PersonID = 101,
                    FirstName = "Jackie",
                    LastName = "Chan",
                    BirthDate = new DateTime(1990, 08, 18)
                };
            }
            else
            {
                person = _context.Persons.SingleOrDefault(p => p.PersonID == id);
            }
            return View(person);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var person = _context.Persons.SingleOrDefault(p => p.PersonID == id);
            if (person == null)
            {
                return NotFound();
            }
            return View("ShowPerson", person);
        }

        public IActionResult EditPerson(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var person = _context.Persons.SingleOrDefault(p => p.PersonID == id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        [HttpPost]
        public IActionResult EditPerson(Person person)
        {
           
            if (ModelState.IsValid)
            {
                repo.Edit(person);
                return RedirectToAction("ShowList");
            }
            else
            {
                return View(person);
            }
        }

        public IActionResult RemovePerson(int? id)
        {
            var person = _context.Persons.SingleOrDefault(p => p.PersonID == id);
            repo.Remove(person);
            return RedirectToAction("ShowList");
        }
        public IActionResult AddPerson()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPerson(Person person)
        {
            if (ModelState.IsValid)
            {
                repo.Add(person);
                return RedirectToAction("ShowList");
            }
            else
            {
                return View(person);
            }
        }
    }
}