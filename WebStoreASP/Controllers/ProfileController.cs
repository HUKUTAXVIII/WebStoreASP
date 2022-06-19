using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using WebStoreASP.Models;


public static class UserOptions
{

    public static User user { set; get; }
    public static MySqlConnection conn { set; get; }


    static UserOptions()
    {
        string connString = "Server=" + "mysql8002.site4now.net" + ";Database=" + "db_a88ae9_shopdb"
              + ";User Id=" + "a88ae9_shopdb" + ";password=" + "pass1234";
        conn = new MySqlConnection(connString);
        user = new User(0, "", "", "", "");
    }


    public static bool GetUser(string login, string password) {
        conn.Open();
        bool result = true;
        try
        {

        
            string sql = $"SELECT * FROM `user` WHERE `username` = '{login}' AND `password`='{password}';";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                int id = Convert.ToInt32(rdr["id"]);
                string username = rdr["username"].ToString();
                string name = rdr["name"].ToString();
                string surname = rdr["surname"].ToString();
                string pass = rdr["password"].ToString();


                user = new User(id,username,name,surname,pass);

                

            }

        }
        catch (Exception)
        {

            result = false;
        }



        conn.Close();

        return result;
    }

    public static bool RegisterUser(string username, string name, string surname, string password) {

        bool result = true; 

        conn.Open();

            string sql = $"INSERT INTO `user`(`id`,`username`, `name`, `surname`, `password`) VALUES (1,'{username}','{name}','{surname}','{password}');";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
        try
        {
        }
        catch (Exception)
        {
            result = false;


        }


        conn.Close();

        return result;

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
            ViewBag.username = string.Empty;
            return View();
        }


        [HttpPost]
        public RedirectResult LogIn(string username, string password)
        {

            if (UserOptions.GetUser(username, password)) {
                return Redirect("/Home/Index");
            }

            return Redirect("/Home/LogIn");
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



    }
}
