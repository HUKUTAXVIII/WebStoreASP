using Microsoft.AspNetCore.Mvc;

namespace WebStoreASP.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ILogger<ProfileController> _logger;

        public ProfileController(ILogger<ProfileController> logger)
        {
            _logger = logger;
        }


        public IActionResult LogIn()
        {

            return View();
        }
        public IActionResult SignIn()
        {

            return View();
        }


    }
}
