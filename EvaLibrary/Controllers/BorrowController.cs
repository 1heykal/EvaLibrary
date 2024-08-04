using EvaLibrary.DbContexts;
using EvaLibrary.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EvaLibrary.Controllers;

public class BorrowController : Controller
{
    private readonly ApplicationDbContext _context;

    public BorrowController(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public IActionResult Index()
    {
        return View(_context.Borrows.
            Include(b => b.Book)
            .Include(b => b.Member)
            .ToList());
    }
    
    public IActionResult Add()
    {
        ViewBag.Books = FillBooksViewBage();
        ViewBag.Members = FillMembersViewBag();
        
        return View();
    }

    [HttpPost]
    public IActionResult Add(Borrow borrow)
    {
        borrow.BorrowDate = DateTime.UtcNow;
        _context.Borrows.Add(borrow);
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Update(int id)
    {
        
        var borrow = _context.Borrows
            .Include(b => b.Book)
            .Include(b => b.Member)
            .FirstOrDefault(b => b.Id == id);
        
        ViewBag.Books = FillBooksViewBage();
        ViewBag.Members = FillMembersViewBag();
        
        return View(borrow);
    }
    
    [HttpPost]
    public IActionResult Update(Borrow borrow)
    {
        _context.Borrows.Update(borrow);
        _context.SaveChanges();
        
        return View();
    }

    public IActionResult Delete(int id)
    {
        var borrow = _context.Borrows
            .Include(b => b.Book)
            .Include(b => b.Member).
            FirstOrDefault(b => b.Id == id);
        
        return View(borrow);
    }
    
    [HttpPost]
    public IActionResult Delete(Borrow borrow)
    {
        _context.Borrows.Remove(borrow);
        _context.SaveChanges();
        
        return RedirectToAction(nameof(Index));
    }

    private SelectList FillBooksViewBage()
    {
        var books = _context.Books.Select(b => new SelectListItem() { Value = b.Id.ToString(), Text = b.Title }).ToList();
        return new SelectList(books, "Value", "Text");
    }

    private SelectList FillMembersViewBag()
    {
        var members = _context.Members.Select(m => new SelectListItem() { Value = m.Id.ToString(), Text = m.Name })
            .ToList();
        
       return new SelectList(members, "Value", "Text");
    }
}