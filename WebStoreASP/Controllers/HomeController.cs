using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebStoreASP.Models;

namespace WebStoreASP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public List<Product> products { get; set; }
        public List<Category> categories { get; set; }
        public List<Author> authors { get; set; }
        public List<Publisher> publishers { get; set; }
        public List<Genre> genres { get; set; }



        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

            products = new List<Product>() {
                new Product(1,"Book1",10.95f,"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",1,1,1,1),
                new Product(2,"Book2",12.95f,"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",1,1,2,1),
                new Product(3,"Book3",15.95f,"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",3,1,3,2),
            };


            categories = new List<Category>() {
                new Category(1,"Художественная литература"),
                new Category(2,"Детская литература"),
                new Category(3,"Познавательная литература"),
                new Category(4,"Бизнес литература"),
            };
            genres = new List<Genre>()
            {
                new Genre(1,"Проза"),
                new Genre(2,"Фентази"),
                new Genre(3,"Поэзия"),
                new Genre(4,"Научная")
            };
            authors = new List<Author>()
            {
                new Author(1,"Николай Гоголь"),
                new Author(2,"Франц Кафка"),
                new Author(3,"Стивен Хокинг")
            };
            publishers = new List<Publisher>() {
                new Publisher(1,"Artbooks"),
                new Publisher(2,"Азбука")
            };



        }

        public IActionResult Index(string q="")
        {
            if (q == null) {
                q = string.Empty;
            }




            ViewBag.Products = products.Where(p => p.name.Contains(q)).ToList();
            ViewBag.Categories = categories;
            ViewBag.Genres = genres;
            ViewBag.Authors = authors;
            ViewBag.Publishers = publishers;

            return View();
        }


        public IActionResult Book(int id)
        {

            id = 1;
            ViewBag.CurrentBook = products.Where(p=>p.id==id).First();


            ViewBag.Products = products;
            ViewBag.Categories = categories;
            ViewBag.Genres = genres;
            ViewBag.Authors = authors;
            ViewBag.Publishers = publishers;

            return View();
        }




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        //[HttpGet]
        //public IActionResult Get(string q) {
        //    ViewBag.Products = products.Where(p => p.name == q).ToList();
        //    ViewBag.Categories = categories;
        //    ViewBag.Genres = genres;
        //    ViewBag.Authors = authors;
        //    ViewBag.Publishers = publishers;

        //    return View();
        //}


    }
}