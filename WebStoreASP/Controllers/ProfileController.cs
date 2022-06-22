using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using WebStoreASP.Models;
using System.Diagnostics;


public static class UserOptions
{

    //public static User user { set; get; }
    public static MySqlConnection conn { set; get; }
    //public static List<int> cart { set; get; }

    static UserOptions()
    {
        string connString = "Server=" + "mysql8002.site4now.net" + ";Database=" + "db_a88ae9_shopdb"
              + ";User Id=" + "a88ae9_shopdb" + ";password=" + "pass1234";
        conn = new MySqlConnection(connString);
    }


    public static int GetUserID(string login, string password) {
        conn.Open();
        int result = -1;
        try
        {

        
            string sql = $"SELECT * FROM `user` WHERE `username` = '{login}' AND `password`='{password}';";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                int id = Convert.ToInt32(rdr["id"]);
                //string username = rdr["username"].ToString();
                //string name = rdr["name"].ToString();
                //string surname = rdr["surname"].ToString();
                //string pass = rdr["password"].ToString();


                // user = new User(id,username,name,surname,pass);

                result = id;
                //HttpContext.Session.SetString("Test", "1");



            }

        }
        catch (Exception)
        {

            result = -1;
        }



        conn.Close();

        return result;
    }
    public static User GetUser(int user_id)
    {
        conn.Open();
        User result = new User(0,"","");
        try
        {


            string sql = $"SELECT * FROM `user` WHERE `id`={user_id};";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                int id = Convert.ToInt32(rdr["id"]);
                string username = rdr["username"].ToString();
                string name = rdr["name"].ToString();
                string surname = rdr["surname"].ToString();
                string pass = rdr["password"].ToString();


                result = new User(id,username,name,surname,pass);

                //HttpContext.Session.SetString("Test", "1");



            }

        }
        catch (Exception)
        {

            
        }



        conn.Close();

        return result;
    }

    public static bool RegisterUser(string username, string name, string surname, string password) {

        bool result = true; 

        conn.Open();

        try
        {
            string sql = $"INSERT INTO `user`(`username`, `name`, `surname`, `password`) VALUES ('{username}','{name}','{surname}','{password}');";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();

        }
        catch (Exception)
        {
            result = false;


        }


        conn.Close();

        return result;

    }

    public static void AddToCart(int book_id,int user_id) {
        conn.Open();

        string sql = $"INSERT INTO `cart`(`user_id`, `product_id`) VALUES ('{user_id}','{book_id}');";
        MySqlCommand cmd = new MySqlCommand(sql, conn);
        cmd.ExecuteNonQuery();
        
        conn.Close();
    }
    public static List<int> GetCart(int user_id) {
        conn.Open();

        string sql = $"SELECT * FROM `cart` WHERE `user_id` = '{user_id}';";
        MySqlCommand cmd = new MySqlCommand(sql, conn);
        MySqlDataReader rdr = cmd.ExecuteReader();
        List<int> cart = new List<int>();
        while (rdr.Read())
        {
            int id = Convert.ToInt32(rdr["id"]);
            int book_id = Convert.ToInt32(rdr["product_id"]);





            cart.Add(book_id);



        }



        conn.Close();

        return cart;


    }

    public static void RemoveFromCart(int book_id,int user_id) {
        conn.Open();
        //DELETE FROM `cart` WHERE `product_id`=pf AND `user_id`=id;

        string sql = $"DELETE FROM `cart` WHERE `product_id`={book_id} AND `user_id`={user_id};";
        MySqlCommand cmd = new MySqlCommand(sql, conn);
        cmd.ExecuteNonQuery();


        conn.Close();
    }


}



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
            if (HttpContext.Session.GetString("UserID") == null) {
                return Redirect("../Profile/LogIn");
            }
            if (HttpContext.Session.GetString("UserID") == string.Empty)
            {
                return Redirect("../Profile/LogIn");
            }

            User user = UserOptions.GetUser(int.Parse(HttpContext.Session.GetString("UserID")));
            ViewBag.username = user.username;

            ViewBag.User = user;

            return View();
        }


        [HttpPost]
        public RedirectResult LogIn(string username, string password)
        {
            int user_id = UserOptions.GetUserID(username, password);
            if (user_id!=-1) {

                HttpContext.Session.SetString("UserID",user_id.ToString());

                


                return Redirect("/Profile/Profile");
            }

            return Redirect("/Profile/LogIn");
        }




        [HttpGet]
        public IActionResult SignIn()
        {
            ViewBag.username = string.Empty;
            return View();
        }

        [HttpPost]
        public RedirectResult SignIn(string username,string name, string surname, string password, string confirm)
        {
            ViewBag.username = string.Empty;
            if (
                (username.Length > 8 && username.Length < 16)&&
                (name.Length > 8 && name.Length < 16) &&
                (surname.Length > 8 && surname.Length < 16) &&
                (password.Length > 8 && password.Length < 16)&&
                (confirm.Length > 8 && confirm.Length < 16)&&
                (confirm==password)
                )
            {
                if (UserOptions.RegisterUser(username, name, surname, password)) {
                    return Redirect("/Profile/LogIn");
                }

            }



            return Redirect("/Profile/SignIn"); 
        }


        [HttpGet]
        public IActionResult Cart() {

            if (HttpContext.Session.GetString("UserID") == null)
            {
                return Redirect("../Profile/LogIn");
            }
            if (HttpContext.Session.GetString("UserID") == string.Empty)
            {
                return Redirect("../Profile/LogIn");
            }


            if (HttpContext.Session.GetString("UserID") != string.Empty)
            {
                ViewBag.username = UserOptions.GetUser(int.Parse(HttpContext.Session.GetString("UserID"))).username;



                ViewBag.cartlist = DBBooks.products.Where((b) => UserOptions.GetCart(int.Parse(HttpContext.Session.GetString("UserID"))).Any((i) => i == b.id)).ToList();

            }
            else
            {
                ViewBag.username = string.Empty;
            }

            ViewBag.Covers = DBBooks.covers;
            ViewBag.Authors = DBBooks.authors;


            return View();
        }
        [HttpPost]
        public IActionResult Cart(int book_id)
        {

            if (HttpContext.Session.GetString("UserID") == null)
            {
                return Redirect("../Profile/LogIn");
            }
            if (HttpContext.Session.GetString("UserID") == string.Empty)
            {
                return Redirect("../Profile/LogIn");
            }



            if (HttpContext.Session.GetString("UserID") != string.Empty)
            {
                ViewBag.username = UserOptions.GetUser(int.Parse(HttpContext.Session.GetString("UserID"))).username;

                UserOptions.RemoveFromCart(book_id, int.Parse(HttpContext.Session.GetString("UserID")));
                ViewBag.cartlist = DBBooks.products.Where((b) => UserOptions.GetCart(int.Parse(HttpContext.Session.GetString("UserID"))).Any((i) => i == b.id)).ToList();
            }
            else
            {
                ViewBag.username = string.Empty;
            }

            ViewBag.Covers = DBBooks.covers;
            ViewBag.Authors = DBBooks.authors;

            //UserOptions.GetCart();

            return View();
        }



    }
}
