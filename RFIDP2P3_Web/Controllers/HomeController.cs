using Microsoft.AspNetCore.Mvc;

namespace RFIDP2P3_Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(String username)
        {
            if (HttpContext.Session.GetString("PIC_ID") != null) return View();
            else return RedirectToAction("Index", "Login");
        }
    }
}