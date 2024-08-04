using EvaLibrary.DbContexts;
using EvaLibrary.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EvaLibrary.Controllers
{
    public class AuthorController(ApplicationDbContext context) : Controller
    {
        public ActionResult Index()
        {
            return View(context.Authors.Include(a => a.Books).ToList());
        }
        public IActionResult Details(int id)
        {
            var author = context.Authors.Include(a => a.Books).FirstOrDefault(a => a.Id == id);
            return View(author);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Author author)
        {
            context.Authors.Add(author);
            context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var author = context.Authors.Find(id);
            return View(author);
        }

        [HttpPost]
        public IActionResult Update(Author author)
        {
            context.Authors.Update(author);
            context.SaveChanges();

            return RedirectToAction(nameof(Details),routeValues: new {id = author.Id});
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var author = context.Authors.Include(a => a.Books).FirstOrDefault(a => a.Id == id);
            return View(author);
        }

        [HttpPost]
        public IActionResult Delete(Author author)
        {
            context.Authors.Remove(author);
            context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}
