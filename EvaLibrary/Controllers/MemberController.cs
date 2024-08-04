using EvaLibrary.DbContexts;
using EvaLibrary.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EvaLibrary.Controllers
{
    public class MemberController : Controller
    {
        private readonly ApplicationDbContext _context;
        public MemberController(ApplicationDbContext context)
        {
            _context = context;
        }
        public ActionResult Index()
        {
            return View(_context.Members.Include(m => m.Borrows).ToList());
        }

        public IActionResult Details(int id)
        {
            var member = _context.Members
                .Include(m => m.Borrows)
                .FirstOrDefault(m => m.Id == id);
            return View(member);
        }

        public IActionResult Add()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Add(Member member)
        {
            member.JoinDate = new DateOnly(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day);
            _context.Members.Add(member);
            _context.SaveChanges();
            
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            var member = _context.Members.Find(id);
            return View(member);
        }

        [HttpPost]
        public IActionResult Update(Member member)
        {
            _context.Members.Update(member);
            _context.SaveChanges();

            return RedirectToAction(nameof(Details), new { id = member.Id });
        }
        
        public IActionResult Delete(int id)
        {
            var member = _context.Members.Find(id);
            return View(member);
        }

        [HttpPost]
        public IActionResult Delete(Member member)
        {
            _context.Members.Remove(member);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}
