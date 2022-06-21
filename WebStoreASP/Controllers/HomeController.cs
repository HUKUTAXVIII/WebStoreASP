using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebStoreASP.Models;
using MySql.Data.MySqlClient;



public static class DBBooks
{
    public static List<Product> products { get; set; }
    public static List<Category> categories { get; set; }
    public static List<Author> authors { get; set; }
    public static List<Publisher> publishers { get; set; }
    public static List<Genre> genres { get; set; }
    public static List<Cover> covers { set; get; }
    public static MySqlConnection conn{set;get;}
    
    static DBBooks()
    {
        string connString = "Server=" + "mysql8002.site4now.net" + ";Database=" + "db_a88ae9_shopdb"
              + ";User Id=" + "a88ae9_shopdb" + ";password=" + "pass1234";
        conn = new MySqlConnection(connString);

        products = new List<Product>();
        categories = new List<Category>();
        authors = new List<Author>();   
        publishers = new List<Publisher>();
        genres = new List<Genre>();
        covers = new List<Cover>();
        

        GetCategory();
        GetPublisher();
        GetAuthor();
        GetGenre();
        GetCover();
        GetProduct();



    }

    public static void GetCategory() {
        conn.Open();
        string sql = "SELECT * FROM `category`;";
        MySqlCommand cmd = new MySqlCommand(sql, conn);
        MySqlDataReader rdr = cmd.ExecuteReader();
        categories = new List<Category>();

        while (rdr.Read())
        {
            //Console.WriteLine(rdr[0] + " -- " + rdr[1]);
            int id = Convert.ToInt32(rdr["id"]);
            string name = rdr["name"].ToString();



            categories.Add(new Category(id, name));



        }

        conn.Close();
    }
    public static void GetPublisher()
    {
        conn.Open();
        publishers = new List<Publisher>();
        string sql = "SELECT * FROM `publisher`;";
        MySqlCommand cmd = new MySqlCommand(sql, conn);
        MySqlDataReader rdr = cmd.ExecuteReader();

        while (rdr.Read())
        {
            //Console.WriteLine(rdr[0] + " -- " + rdr[1]);
            int id = Convert.ToInt32(rdr["id"]);
            string name = rdr["name"].ToString();



            publishers.Add(new Publisher(id, name));



        }

        conn.Close();
    }
    public static void GetAuthor()
    {
        conn.Open();

        string sql = "SELECT * FROM `author`;";
        MySqlCommand cmd = new MySqlCommand(sql, conn);
        MySqlDataReader rdr = cmd.ExecuteReader();
        authors = new List<Author>();
        while (rdr.Read())
        {
            //Console.WriteLine(rdr[0] + " -- " + rdr[1]);
            int id = Convert.ToInt32(rdr["id"]);
            string name = rdr["name"].ToString();



            authors.Add(new Author(id, name));



        }

        conn.Close();
    }
    public static void GetGenre()
    {
        conn.Open();

        string sql = "SELECT * FROM `genre`;";
        MySqlCommand cmd = new MySqlCommand(sql, conn);
        MySqlDataReader rdr = cmd.ExecuteReader();
        genres = new List<Genre>();
        while (rdr.Read())
        {
            //Console.WriteLine(rdr[0] + " -- " + rdr[1]);
            int id = Convert.ToInt32(rdr["id"]);
            string name = rdr["name"].ToString();



            genres.Add(new Genre(id, name));



        }

        conn.Close();
    }
    public static void GetCover()
    {
        conn.Open();

        string sql = "SELECT * FROM `cover`;";
        MySqlCommand cmd = new MySqlCommand(sql, conn);
        MySqlDataReader rdr = cmd.ExecuteReader();
        covers = new List<Cover>();
        while (rdr.Read())
        {
            //Console.WriteLine(rdr[0] + " -- " + rdr[1]);
            int id = Convert.ToInt32(rdr["id"]);
            string name = rdr["name"].ToString();
            string type = rdr["type"].ToString();
            var data = (byte[])rdr["content"];
            string content = "data:image/jpg;base64,"+Convert.ToBase64String(data,0,data.Length);



            covers.Add(new Cover(id, name,type,content));



        }

        conn.Close();
    }

    public static void GetProduct() {
        conn.Open();
        products = new List<Product>();
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
            int cover = Convert.ToInt32(rdr["cover_id"]);


            products.Add(new Product(id, name, price, description, auth, pub, genre, cat,cover));



        }

        conn.Close();

    }




    public static void AddCover(string name,string type,byte[] content)
    {
        conn.Open();

        byte[] data = content;

        MySqlCommand command = new MySqlCommand($"INSERT INTO `cover`(`name`, `type`, `content`) VALUES ('{name}','{type}',@Data);", conn);
        command.Parameters.AddWithValue("@Data",data);

        command.ExecuteNonQuery();


        conn.Close();
        GetCover();
    }
    public static void AddBook(string name,float price,string description,int author_id,int publisher_id, int genre_id,int category_id,int cover_id) {

        conn.Open();



        MySqlCommand command = new MySqlCommand($"INSERT INTO `product`(`name`, `price`, `description`, `author_id`, `publisher_id`, `genre_id`, `category_id`, `cover_id`) VALUES ('{name}','{price}','{description}','{author_id}','{publisher_id}','{genre_id}','{category_id}','{cover_id}')", conn);

        command.ExecuteNonQuery();

        conn.Close();
        GetProduct();

    }

    public static void AddAuthor(string name)
    {

        conn.Open();



        MySqlCommand command = new MySqlCommand($"INSERT INTO `author`(`name`) VALUES ('{name}')", conn);

        command.ExecuteNonQuery();

        conn.Close();
        GetAuthor();

    }
    public static void AddPublisher(string name)
    {

        conn.Open();



        MySqlCommand command = new MySqlCommand($"INSERT INTO `publisher`(`name`) VALUES ('{name}')", conn);

        command.ExecuteNonQuery();

        conn.Close();
        GetPublisher();

    }
    public static void AddGenre(string name)
    {

        conn.Open();



        MySqlCommand command = new MySqlCommand($"INSERT INTO `genre`(`name`) VALUES ('{name}')", conn);

        command.ExecuteNonQuery();

        conn.Close();
        GetGenre();

    }
    public static void AddCategory(string name)
    {

        conn.Open();



        MySqlCommand command = new MySqlCommand($"INSERT INTO `category`(`name`) VALUES ('{name}')", conn);

        command.ExecuteNonQuery();

        conn.Close();
        GetCategory();

    }

    public static void DeleteAuthor(int id) {
        conn.Open();
        MySqlCommand command = new MySqlCommand($"DELETE FROM `author` WHERE `id` = {id};", conn);

        command.ExecuteNonQuery();
        conn.Close();
        GetAuthor();
    }
    public static void DeletePublisher(int id)
    {
        conn.Open();
        MySqlCommand command = new MySqlCommand($"DELETE FROM `publisher` WHERE `id` = {id};", conn);

        command.ExecuteNonQuery();
        conn.Close();
        GetPublisher();
    }
    public static void DeleteGenre(int id)
    {
        conn.Open();
        MySqlCommand command = new MySqlCommand($"DELETE FROM `genre` WHERE `id` = {id};", conn);

        command.ExecuteNonQuery();
        conn.Close();
        GetGenre();
    }
    public static void DeleteCategory(int id)
    {
        conn.Open();
        MySqlCommand command = new MySqlCommand($"DELETE FROM `category` WHERE `id` = {id};", conn);

        command.ExecuteNonQuery();
        conn.Close();
        GetCategory();
    }




    public static void DeleteBook(int id) {
        conn.Open();
        //SELECT * FROM `product` WHERE `id` = [id];
        MySqlCommand command = new MySqlCommand($"DELETE FROM `product` WHERE `id` = {id};", conn);

        command.ExecuteNonQuery();


        conn.Close();
        GetProduct();
    }
    public static void DeleteCover(int id)
    {
        conn.Open();
        MySqlCommand command = new MySqlCommand($"DELETE FROM `cover` WHERE `id` = {id};", conn);

        command.ExecuteNonQuery();


        conn.Close();
        GetCover();
    }


}



namespace WebStoreASP.Controllers
{



    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;




        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;







        }

        [HttpGet]
        public IActionResult Index(string q = "")
        {
            if (q == null) {
                q = string.Empty;
            }

            //HttpContext.Session.SetString("Test","1");
            if (HttpContext.Session.GetString("UserID") == null)
            {
                HttpContext.Session.SetString("UserID", "");
            }




            ViewBag.Products = DBBooks.products.Where(p => p.name.ToLower().Contains(q.ToLower())).ToList();
            ViewBag.Categories = DBBooks.categories;
            ViewBag.Genres = DBBooks.genres;
            ViewBag.Authors = DBBooks.authors;
            ViewBag.Covers = DBBooks.covers;
            ViewBag.Publishers = DBBooks.publishers;

            ViewBag.username = HttpContext.Session.GetString("UserID");

            ViewBag.Aus = new List<string>();
            ViewBag.Cat = new List<string>();
            ViewBag.Gen = new List<string>();
            ViewBag.Pub = new List<string>();

            return View();
        }


        [HttpPost]
        public IActionResult Index(string[] category, string[] genre, string[] author, string[] publisher)
        {

            if (HttpContext.Session.GetString("UserID") == null) {
                HttpContext.Session.SetString("UserID","");
            }


            var data_a = author.ToList();
            var data_c = category.ToList();
            var data_g = genre.ToList();
            var data_p = publisher.ToList();


            ViewBag.Aus = data_a;
            ViewBag.Cat = data_c;
            ViewBag.Gen = data_g;
            ViewBag.Pub = data_p;


            ViewBag.Products = DBBooks.products
                .Where((item)=>data_a.Any(a=>a==item.author_id.ToString()) || data_a.Count==0 )
                .Where((item) => data_c.Any(c => c == item.category_id.ToString()) || data_c.Count == 0)
                .Where((item) => data_g.Any(g => g == item.genre_id.ToString()) || data_g.Count == 0)
                .Where((item) => data_p.Any(p => p == item.publisher_id.ToString()) || data_p.Count == 0).ToList();
            ViewBag.Categories = DBBooks.categories;
            ViewBag.Genres = DBBooks.genres;
            ViewBag.Authors = DBBooks.authors;
            ViewBag.Covers = DBBooks.covers;
            ViewBag.Publishers = DBBooks.publishers;

            ViewBag.username = HttpContext.Session.GetString("UserID");


            return View();
        }



        [HttpGet]
        public IActionResult Book(int id)
        {

            ViewBag.username = HttpContext.Session.GetString("UserID");
            ViewBag.CurrentBook = DBBooks.products.Where(p=>p.id==id).First();


            ViewBag.Products = DBBooks.products;
            ViewBag.Categories = DBBooks.categories;
            ViewBag.Genres = DBBooks.genres;
            ViewBag.Authors = DBBooks.authors;
            ViewBag.Covers = DBBooks.covers;
            ViewBag.Publishers = DBBooks.publishers;

            return View();
        }

        [HttpPost]
        public IActionResult Book(int id,int other=0)
        {
            ViewBag.username = HttpContext.Session.GetString("UserID");
            //UserOptions.cart.Add(id);

            UserOptions.AddToCart(id,int.Parse(HttpContext.Session.GetString("UserID")));

            

            

          


            ViewBag.CurrentBook = DBBooks.products.Where(p => p.id == id).First();

            ViewBag.Products = DBBooks.products;
            ViewBag.Categories = DBBooks.categories;
            ViewBag.Genres = DBBooks.genres;
            ViewBag.Authors = DBBooks.authors;
            ViewBag.Covers = DBBooks.covers;
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