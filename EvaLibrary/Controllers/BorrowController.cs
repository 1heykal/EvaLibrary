using EvaLibrary.DbContexts;
using EvaLibrary.Entities;
using EvaLibrary.Services.BookService;
using EvaLibrary.Services.BorrowService;
using EvaLibrary.Services.MemberService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EvaLibrary.Controllers;

public class BorrowController : Controller
{
    private readonly IBorrowService _borrowService;
    private readonly IBookService _bookService;
    private readonly IMemberService _memberService;



    public BorrowController(IBorrowService borrowService, IBookService bookService, IMemberService memberService)
    {
        _borrowService = borrowService ?? throw new ArgumentNullException(nameof(borrowService));
        _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
        _memberService = memberService ?? throw new ArgumentNullException(nameof(memberService));;

    }
    public IActionResult Index()
    {
        return View(_borrowService.GetAllBorrows());
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
       _borrowService.AddBorrow(borrow);

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Update(int id)
    {

        var borrow = _borrowService.GetBorrowById(id);
        
        ViewBag.Books = FillBooksViewBage();
        ViewBag.Members = FillMembersViewBag();
        
        return View(borrow);
    }
    
    [HttpPost]
    public IActionResult Update(Borrow borrow)
    {
        _borrowService.UpdateBorrow(borrow);
        return View();
    }

    public IActionResult Delete(int id)
    {
        var borrow = _borrowService.GetBorrowById(id);
        return View(borrow);
    }
    
    [HttpPost]
    public IActionResult Delete(Borrow borrow)
    {
        _borrowService.DeleteBorrow(borrow);
        return RedirectToAction(nameof(Index));
    }

    private SelectList FillBooksViewBage()
    {
        var books = _bookService.GetAllBooks()
            .Select(b => new {BookId = b.Id.ToString(), b.Title })
            .Select(b => new SelectListItem { Value = b.BookId, Text = b.Title })
            .ToList();
        
        return new SelectList(books, "Value", "Text");
    }

    private SelectList FillMembersViewBag()
    {
        var members = _memberService.GetAllMembers()
            .Select(m => new { MemberId = m.Id, m.Name})
            .Select(m => new SelectListItem() { Value = m.MemberId.ToString(), Text = m.Name })
            .ToList();
        
       return new SelectList(members, "Value", "Text");
    }
}