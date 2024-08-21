using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Data;

namespace MyApp.Controllers
{
    public class PersonController : Controller
    {
        private readonly MyApp.Data.AppDbContext _context;

        public PersonController(MyApp.Data.AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (_context.Persons != null)
            {
                List<Person> persons = await _context.Persons.ToListAsync();
                return View(persons);
            }
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Person person)
        {
            if (!ModelState.IsValid || _context.Persons == null || person == null)
            {
                return View();
            }
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Persons == null)
            {
                return NotFound();
            }

            var person = await _context.Persons.FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }
            Person personResult = person;
            return View(personResult);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Person person)
        {
            if (!ModelState.IsValid || _context.Persons == null || person == null)
            {
                return View();
            }
            _context.Attach(person).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Persons == null)
            {
                return NotFound();
            }

            var person = await _context.Persons.FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }
            Person personResult = person;
            return View(personResult);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Person person)
        {
            if (!ModelState.IsValid || _context.Persons == null || person == null)
            {
                return View();
            }
            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Persons == null)
            {
                return NotFound();
            }

            var person = await _context.Persons.FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }
            Person personResult = person;
            return View(personResult);
        }
    }
}
