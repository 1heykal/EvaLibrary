using EvaLibrary.DbContexts;
using EvaLibrary.Entities;
using EvaLibrary.Services.AuthorService;
using EvaLibrary.Services.BookService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EvaLibrary.Controllers;

public class BookController : Controller
{
    private readonly IBookService _bookService;
    private readonly IAuthorService _authorService;
    public BookController(IBookService bookService, IAuthorService authorService)
    {
        _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
        _authorService = authorService ?? throw new ArgumentNullException(nameof(authorService));
    }
    public IActionResult Index()
    {
        return View(_bookService.GetAllBooks());
    }
    
    public IActionResult Add()
    {
        ViewBag.Authors = FillAuthorsViewBag();
        return View();
    }

    [HttpPost]
    public IActionResult Add(Book book)
    { 
        _bookService.AddBook(book);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Update(int id)
    {
        var book = _bookService.GetBookById(id);
        ViewBag.Authors = FillAuthorsViewBag();

        return View(book);
    }
    
    [HttpPost]
    public IActionResult Update(Book book)
    {
       _bookService.UpdateBook(book);
       return View();
    }

    public IActionResult Delete(int id)
    {
        var book = _bookService.GetBookById(id);
        return View(book);
    }
    
    [HttpPost]
    public IActionResult Delete(Book book)
    {
        _bookService.DeleteBook(book);
        return RedirectToAction(nameof(Index));
    }

    private SelectList FillAuthorsViewBag()
    {
        var authors = _authorService.GetAllAuthors().Select(a => new SelectListItem() { Value = a.Id.ToString(), Text = a.Name }).ToList();
        return new SelectList(authors, "Value", "Text");
    }
}