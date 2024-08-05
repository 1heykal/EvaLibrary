using EvaLibrary.DbContexts;
using EvaLibrary.Entities;
using EvaLibrary.Services.MemberService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EvaLibrary.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;
        public MemberController(IMemberService memberService)
        {
            _memberService = memberService ?? throw new ArgumentNullException(nameof(memberService));
        }
        public ActionResult Index()
        {
            return View(_memberService.GetAllMembers());
        }

        public IActionResult Details(int id)
        {
            var member = _memberService.GetMemberById(id);
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
            _memberService.AddMember(member);
            
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            var member = _memberService.GetMemberById(id);
            return View(member);
        }

        [HttpPost]
        public IActionResult Update(Member member)
        {
            _memberService.UpdateMember(member);
            return RedirectToAction(nameof(Details), new { id = member.Id });
        }
        
        public IActionResult Delete(int id)
        {
            var member = _memberService.GetMemberById(id);
            return View(member);
        }

        [HttpPost]
        public IActionResult Delete(Member member)
        {
            _memberService.DeleteMember(member);
            return RedirectToAction(nameof(Index));
        }

    }
}
