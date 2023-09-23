using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UsersController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}