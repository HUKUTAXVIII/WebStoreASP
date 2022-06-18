using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebStoreASP.Models;
using MySql.Data.MySqlClient;

public static class UserOptions
{
    public static string username { set; get; }
    public static string password { set; get; }

    static UserOptions()
    {
        username = string.Empty;
    }
}


public static class DBBooks
{
    public static List<Product> products { get; set; }
    public static List<Category> categories { get; set; }
    public static List<Author> authors { get; set; }
    public static List<Publisher> publishers { get; set; }
    public static List<Genre> genres { get; set; }
    public static MySqlConnection conn{set;get;}
    
    static DBBooks()
    {
        string connString = "Server=" + "mysql8002.site4now.net" + ";Database=" + "db_a88ae9_shopdb"
              + ";User Id=" + "a88ae9_shopdb" + ";password=" + "pass1234";
        conn = new MySqlConnection(connString);


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
        products = new List<Product>();


        conn.Open();

        string sql = "SELECT * FROM `product`;";
        MySqlCommand cmd = new MySqlCommand(sql, conn);
        MySqlDataReader rdr = cmd.ExecuteReader();

        while (rdr.Read())
        {
            //Console.WriteLine(rdr[0] + " -- " + rdr[1]);
            int id = Convert.ToInt32(rdr["id"]);
            string name = rdr["name"].ToString();
            float price = float.Parse(rdr["price"].ToString());
            string description = rdr["description"].ToString();
            int auth = Convert.ToInt32(rdr["author_id"]);
            int pub = Convert.ToInt32(rdr["publisher_id"]);
            int genre = Convert.ToInt32(rdr["genre_id"]);
            int cat = Convert.ToInt32(rdr["category_id"]);


            products.Add(new Product(1, name,12.2f,"123232",1,1,1,1));



        }

        conn.Close();

    }
}



namespace WebStoreASP.Controllers
{
    


    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //public List<Product> products { get; set; }
        //public List<Category> categories { get; set; }
        //public List<Author> authors { get; set; }
        //public List<Publisher> publishers { get; set; }
        //public List<Genre> genres { get; set; }



        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

            //products = new List<Product>() {
            //    new Product(1,"Book1",10.95f,"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",1,1,1,1),
            //    new Product(2,"Book2",12.95f,"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",1,1,2,1),
            //    new Product(3,"Book3",15.95f,"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",3,1,3,2),
            //};


            //categories = new List<Category>() {
            //    new Category(1,"Художественная литература"),
            //    new Category(2,"Детская литература"),
            //    new Category(3,"Познавательная литература"),
            //    new Category(4,"Бизнес литература"),
            //};
            //genres = new List<Genre>()
            //{
            //    new Genre(1,"Проза"),
            //    new Genre(2,"Фентази"),
            //    new Genre(3,"Поэзия"),
            //    new Genre(4,"Научная")
            //};
            //authors = new List<Author>()
            //{
            //    new Author(1,"Николай Гоголь"),
            //    new Author(2,"Франц Кафка"),
            //    new Author(3,"Стивен Хокинг")
            //};
            //publishers = new List<Publisher>() {
            //    new Publisher(1,"Artbooks"),
            //    new Publisher(2,"Азбука")
            //};
            //UserOptions.username = string.Empty;






        }

        public IActionResult Index(string q="")
        {
            if (q == null) {
                q = string.Empty;
            }




            ViewBag.Products = DBBooks.products.Where(p => p.name.Contains(q)).ToList();
            ViewBag.Categories = DBBooks.categories;
            ViewBag.Genres = DBBooks.genres;
            ViewBag.Authors = DBBooks.authors;
            ViewBag.Publishers = DBBooks.publishers;

            ViewBag.username = UserOptions.username;
            

            return View();
        }


        public IActionResult Book(int id)
        {

        
            ViewBag.CurrentBook = DBBooks.products.Where(p=>p.id==id).First();


            ViewBag.Products = DBBooks.products;
            ViewBag.Categories = DBBooks.categories;
            ViewBag.Genres = DBBooks.genres;
            ViewBag.Authors = DBBooks.authors;
            ViewBag.Publishers = DBBooks.publishers;

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