using Microsoft.AspNetCore.Mvc;

namespace RFIDP2P3_Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(String username)
        {
            ViewBag.Username = username;
            return View();
        }
    }
}