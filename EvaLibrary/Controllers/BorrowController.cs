using Microsoft.AspNetCore.Mvc;

namespace EvaLibrary.Controllers;

public class BorrowController : Controller
{
    public IActionResult Index()
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