using Microsoft.AspNetCore.Mvc;

namespace EvaLibrary.Controllers
{
    public class AuthorController : Controller
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
