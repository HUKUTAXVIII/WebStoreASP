using Microsoft.AspNetCore.Mvc;
using WebStoreASP.Models;

namespace WebStoreASP.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ILogger<ProfileController> _logger;

        public ProfileController(ILogger<ProfileController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult LogIn()
        {
            ViewBag.username = string.Empty;
            return View();
        }
        public IActionResult Profile()
        {
            ViewBag.username = string.Empty;
            return View();
        }


        [HttpPost]
        public RedirectResult LogIn(string login, string password)
        {

            ViewBag.user = new User(0, login, password);
            UserOptions.username = login;

            return Redirect("/Home/Index");
        }





        public IActionResult SignIn()
        {

            return View();
        }


    }
}
