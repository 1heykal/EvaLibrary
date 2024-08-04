using Microsoft.AspNetCore.Mvc;

namespace EvaLibrary.Controllers
{
    public class MemberController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Update()
        {
            return View();
        }
        
        public IActionResult Delete()
        {
            return View();
        }

    }
}
