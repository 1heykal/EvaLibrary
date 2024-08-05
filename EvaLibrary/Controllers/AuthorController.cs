using EvaLibrary.DbContexts;
using EvaLibrary.Entities;
using EvaLibrary.Services.AuthorService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EvaLibrary.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService ?? throw new ArgumentNullException(nameof(authorService));
        }
        public ActionResult Index()
        {
            return View(_authorService.GetAllAuthors());
        }
        public IActionResult Details(int id)
        {
            var author =_authorService.GetAuthorById(id);
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
            _authorService.AddAuthor(author);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var author = _authorService.GetAuthorById(id);
            return View(author);
        }

        [HttpPost]
        public IActionResult Update(Author author)
        {
            _authorService.UpdateAuthor(author);
            return RedirectToAction(nameof(Details),routeValues: new {id = author.Id});
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var author = _authorService.GetAuthorById(id);
            return View(author);
        }

        [HttpPost]
        public IActionResult Delete(Author author)
        {
            _authorService.DeleteAuthor(author);
            return RedirectToAction(nameof(Index));
        }

    }
}
