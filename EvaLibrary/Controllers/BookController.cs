using EvaLibrary.DbContexts;
using EvaLibrary.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EvaLibrary.Controllers;

public class BookController : Controller
{
    private readonly ApplicationDbContext _context;

    public BookController(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public IActionResult Index()
    {
        return View(_context.Books.Include(b => b.Author).ToList());
    }
    
    public IActionResult Add()
    {
        ViewBag.Authors = FillAuthorsViewBag();
        return View();
    }

    [HttpPost]
    public IActionResult Add(Book book)
    {
        _context.Books.Add(book);
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Update(int id)
    {
        var book = _context.Books.Include(b => b.Author).FirstOrDefault(b => b.Id == id);
        ViewBag.Authors = FillAuthorsViewBag();

        return View(book);
    }
    
    [HttpPost]
    public IActionResult Update(Book book)
    {
        _context.Books.Update(book);
        _context.SaveChanges();
        return View();
    }

    public IActionResult Delete(int id)
    {
        var book = _context.Books.Include(b => b.Author).FirstOrDefault(b => b.Id == id);
        return View(book);
    }
    
    [HttpPost]
    public IActionResult Delete(Book book)
    {
        _context.Books.Remove(book);
        _context.SaveChanges();
        
        return RedirectToAction(nameof(Index));
    }

    private SelectList FillAuthorsViewBag()
    {
        var authors = _context.Authors.Select(a => new SelectListItem() { Value = a.Id.ToString(), Text = a.Name }).ToList();
        return new SelectList(authors, "Value", "Text");
    }
}