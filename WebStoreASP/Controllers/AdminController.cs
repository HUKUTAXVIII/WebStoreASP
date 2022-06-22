using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebStoreASP.Models;
using MySql.Data.MySqlClient;
using System.IO;
using System.Text;

namespace WebStoreASP.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;


        public AdminController(ILogger<AdminController> logger)
        {
            _logger = logger;
                
        }

        [HttpGet]
        public IActionResult AddBook()
        {

            if (HttpContext.Session.GetString("UserID") != null)
            {
                if (HttpContext.Session.GetString("UserID") != string.Empty)
                {
                    User user = UserOptions.GetUser(int.Parse(HttpContext.Session.GetString("UserID")));
                    ViewBag.username = user.username;
                }
                else
                {
                    ViewBag.username = string.Empty;
                }
            }
            else
            {
                ViewBag.username = string.Empty;
            }




            ViewBag.Categories = DBBooks.categories;
            ViewBag.Genres = DBBooks.genres;
            ViewBag.Authors = DBBooks.authors;
            ViewBag.Covers = DBBooks.covers;
            ViewBag.Publishers = DBBooks.publishers;





            return View();
        }

        [HttpPost]
        public IActionResult AddBook(IFormFile file,string name,string price,string description,int author,int publisher,int category,int genre)
        {
            ViewBag.Categories = DBBooks.categories;
            ViewBag.Genres = DBBooks.genres;
            ViewBag.Authors = DBBooks.authors;
            ViewBag.Covers = DBBooks.covers;
            ViewBag.Publishers = DBBooks.publishers;

            if (file != null)
            {
                //ViewBag.FileName = file.FileName;
                //var data = new StringBuilder();
                //using (var reader = new StreamReader(file.OpenReadStream()))
                //{
                //    while (reader.Peek() >= 0)
                //    {
                //        data.AppendLine(reader.ReadLine());
                //    }

                //    var content = Encoding.UTF8.GetBytes(data.ToString());
                //    DBBooks.AddCover(file.FileName, "jpg", content);
                //}

                if (file.Length >= 50000) {
                    return View();
                }
                if (new FileInfo(file.FileName).Extension != ".jpg") {
                    return View();
                }

                string uploads = Path.Combine("wwwroot/Files");
                if (file.Length > 0)
                {
                    string filePath = Path.Combine(uploads, file.FileName);
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                       file.CopyTo(fileStream);
                    }

                    var data = System.IO.File.ReadAllBytes(filePath);

                    

                    DBBooks.AddCover(file.FileName, "jpg", data);

                    DBBooks.AddBook(name,float.Parse(price),description,author,publisher,genre,category,DBBooks.covers.Last().id);

                    System.IO.File.Delete(filePath);
                }




            }
                if (HttpContext.Session.GetString("UserID") != null)
                {
                    if (HttpContext.Session.GetString("UserID") != string.Empty)
                    {
                        User user = UserOptions.GetUser(int.Parse(HttpContext.Session.GetString("UserID")));
                        ViewBag.username = user.username;
                    }
                    else
                    {
                        ViewBag.username = string.Empty;
                    }
                }
                else
                {
                    ViewBag.username = string.Empty;
                }

                

            return View();
        }

        [HttpGet]
        public IActionResult DeleteBook()
        {


            if (HttpContext.Session.GetString("UserID") != null)
            {
                if (HttpContext.Session.GetString("UserID") != string.Empty)
                {
                    User user = UserOptions.GetUser(int.Parse(HttpContext.Session.GetString("UserID")));
                    ViewBag.username = user.username;
                }
                else
                {
                    ViewBag.username = string.Empty;
                }
            }
            else
            {
                ViewBag.username = string.Empty;
            }

            ViewBag.Products = DBBooks.products;
            ViewBag.Categories = DBBooks.categories;
            ViewBag.Genres = DBBooks.genres;
            ViewBag.Authors = DBBooks.authors;
            ViewBag.Covers = DBBooks.covers;
            ViewBag.Publishers = DBBooks.publishers;

            return View();
        }

        [HttpPost]
        public IActionResult DeleteBook(int book_id)
        {
            int CoverId = DBBooks.products.Find(p => p.id == book_id).cover_id;

            DBBooks.DeleteBook(book_id);
            DBBooks.DeleteCover(CoverId);

            if (HttpContext.Session.GetString("UserID") != null)
            {
                if (HttpContext.Session.GetString("UserID") != string.Empty)
                {
                    User user = UserOptions.GetUser(int.Parse(HttpContext.Session.GetString("UserID")));
                    ViewBag.username = user.username;
                }
                else
                {
                    ViewBag.username = string.Empty;
                }
            }
            else
            {
                ViewBag.username = string.Empty;
            }


            ViewBag.Products = DBBooks.products;
            ViewBag.Categories = DBBooks.categories;
            ViewBag.Genres = DBBooks.genres;
            ViewBag.Authors = DBBooks.authors;
            ViewBag.Covers = DBBooks.covers;
            ViewBag.Publishers = DBBooks.publishers;

            return View();
        }




        [HttpGet]
        public IActionResult Menu()
        {
            if (HttpContext.Session.GetString("UserID") != null)
            {
                if (HttpContext.Session.GetString("UserID") != string.Empty)
                {
                    User user = UserOptions.GetUser(int.Parse(HttpContext.Session.GetString("UserID")));
                    ViewBag.username = user.username;
                }
                else
                {
                    ViewBag.username = string.Empty;
                }
            }
            else {
                ViewBag.username = string.Empty;    
            }

            



            return View();
        }

        [HttpGet]
        public IActionResult AddAuthor()
        {
            if (HttpContext.Session.GetString("UserID") != null)
            {
                if (HttpContext.Session.GetString("UserID") != string.Empty)
                {
                    User user = UserOptions.GetUser(int.Parse(HttpContext.Session.GetString("UserID")));
                    ViewBag.username = user.username;
                }
                else
                {
                    ViewBag.username = string.Empty;
                }
            }
            else
            {
                ViewBag.username = string.Empty;
            }
            return View();
        }

        [HttpPost]
        public IActionResult AddAuthor(string name)
        {
            if (HttpContext.Session.GetString("UserID") != null)
            {
                if (HttpContext.Session.GetString("UserID") != string.Empty)
                {
                    User user = UserOptions.GetUser(int.Parse(HttpContext.Session.GetString("UserID")));
                    ViewBag.username = user.username;
                }
                else
                {
                    ViewBag.username = string.Empty;
                }
            }
            else
            {
                ViewBag.username = string.Empty;
            }


            DBBooks.AddAuthor(name);
            return View();
        }

        [HttpPost]
        public IActionResult DeleteAuthor(int id)
        {
            if (HttpContext.Session.GetString("UserID") != null)
            {
                if (HttpContext.Session.GetString("UserID") != string.Empty)
                {
                    User user = UserOptions.GetUser(int.Parse(HttpContext.Session.GetString("UserID")));
                    ViewBag.username = user.username;
                }
                else
                {
                    ViewBag.username = string.Empty;
                }
            }
            else
            {
                ViewBag.username = string.Empty;
            }

            if (DBBooks.products.Where(p => p.author_id == id).Count() == 0)
            {
                DBBooks.DeleteAuthor(id);
            }
            ViewBag.Authors = DBBooks.authors;
            return View();
        }

        [HttpGet]
        public IActionResult DeleteAuthor()
        {

            if (HttpContext.Session.GetString("UserID") != null)
            {
                if (HttpContext.Session.GetString("UserID") != string.Empty)
                {
                    User user = UserOptions.GetUser(int.Parse(HttpContext.Session.GetString("UserID")));
                    ViewBag.username = user.username;
                }
                else
                {
                    ViewBag.username = string.Empty;
                }
            }
            else
            {
                ViewBag.username = string.Empty;
            }


            ViewBag.Authors = DBBooks.authors;
            return View();
        }




        [HttpGet]
        public IActionResult AddPublisher()
        {
            if (HttpContext.Session.GetString("UserID") != null)
            {
                if (HttpContext.Session.GetString("UserID") != string.Empty)
                {
                    User user = UserOptions.GetUser(int.Parse(HttpContext.Session.GetString("UserID")));
                    ViewBag.username = user.username;
                }
                else
                {
                    ViewBag.username = string.Empty;
                }
            }
            else
            {
                ViewBag.username = string.Empty;
            }


            return View();
        }

        [HttpPost]
        public IActionResult AddPublisher(string name)
        {

            if (HttpContext.Session.GetString("UserID") != null)
            {
                if (HttpContext.Session.GetString("UserID") != string.Empty)
                {
                    User user = UserOptions.GetUser(int.Parse(HttpContext.Session.GetString("UserID")));
                    ViewBag.username = user.username;
                }
                else
                {
                    ViewBag.username = string.Empty;
                }
            }
            else
            {
                ViewBag.username = string.Empty;
            }


            DBBooks.AddPublisher(name);
            return View();
        }

        [HttpPost]
        public IActionResult DeletePublisher(int id)
        {

            if (HttpContext.Session.GetString("UserID") != null)
            {
                if (HttpContext.Session.GetString("UserID") != string.Empty)
                {
                    User user = UserOptions.GetUser(int.Parse(HttpContext.Session.GetString("UserID")));
                    ViewBag.username = user.username;
                }
                else
                {
                    ViewBag.username = string.Empty;
                }
            }
            else
            {
                ViewBag.username = string.Empty;
            }


            if (DBBooks.products.Where(p => p.publisher_id == id).Count() == 0)
            {
                DBBooks.DeletePublisher(id);
            }
            ViewBag.Publishers = DBBooks.publishers;
            return View();
        }


        [HttpGet]
        public IActionResult DeletePublisher()
        {
            if (HttpContext.Session.GetString("UserID") != null)
            {
                if (HttpContext.Session.GetString("UserID") != string.Empty)
                {
                    User user = UserOptions.GetUser(int.Parse(HttpContext.Session.GetString("UserID")));
                    ViewBag.username = user.username;
                }
                else
                {
                    ViewBag.username = string.Empty;
                }
            }
            else
            {
                ViewBag.username = string.Empty;
            }




            ViewBag.Publishers = DBBooks.publishers;
            return View();
        }




        [HttpGet]
        public IActionResult AddCategory()
        {
            if (HttpContext.Session.GetString("UserID") != null)
            {
                if (HttpContext.Session.GetString("UserID") != string.Empty)
                {
                    User user = UserOptions.GetUser(int.Parse(HttpContext.Session.GetString("UserID")));
                    ViewBag.username = user.username;
                }
                else
                {
                    ViewBag.username = string.Empty;
                }
            }
            else
            {
                ViewBag.username = string.Empty;
            }
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(string name)
        {
            if (HttpContext.Session.GetString("UserID") != null)
            {
                if (HttpContext.Session.GetString("UserID") != string.Empty)
                {
                    User user = UserOptions.GetUser(int.Parse(HttpContext.Session.GetString("UserID")));
                    ViewBag.username = user.username;
                }
                else
                {
                    ViewBag.username = string.Empty;
                }
            }
            else
            {
                ViewBag.username = string.Empty;
            }
            DBBooks.AddCategory(name);
            return View();
        }

        [HttpPost]
        public IActionResult DeleteCategory(int id)
        {
            if (HttpContext.Session.GetString("UserID") != null)
            {
                if (HttpContext.Session.GetString("UserID") != string.Empty)
                {
                    User user = UserOptions.GetUser(int.Parse(HttpContext.Session.GetString("UserID")));
                    ViewBag.username = user.username;
                }
                else
                {
                    ViewBag.username = string.Empty;
                }
            }
            else
            {
                ViewBag.username = string.Empty;
            }


            if (DBBooks.products.Where(p => p.category_id == id).Count() == 0)
            {
                DBBooks.DeleteCategory(id);
            }


            ViewBag.Categories = DBBooks.categories;
            return View();
        }


        [HttpGet]
        public IActionResult DeleteCategory()
        {
            if (HttpContext.Session.GetString("UserID") != null)
            {
                if (HttpContext.Session.GetString("UserID") != string.Empty)
                {
                    User user = UserOptions.GetUser(int.Parse(HttpContext.Session.GetString("UserID")));
                    ViewBag.username = user.username;
                }
                else
                {
                    ViewBag.username = string.Empty;
                }
            }
            else
            {
                ViewBag.username = string.Empty;
            }

            ViewBag.Categories = DBBooks.categories;
            return View();
        }



        [HttpGet]
        public IActionResult AddGenre()
        {

            if (HttpContext.Session.GetString("UserID") != null)
            {
                if (HttpContext.Session.GetString("UserID") != string.Empty)
                {
                    User user = UserOptions.GetUser(int.Parse(HttpContext.Session.GetString("UserID")));
                    ViewBag.username = user.username;
                }
                else
                {
                    ViewBag.username = string.Empty;
                }
            }
            else
            {
                ViewBag.username = string.Empty;
            }

            return View();
        }

        [HttpPost]
        public IActionResult AddGenre(string name)
        {
            if (HttpContext.Session.GetString("UserID") != null)
            {
                if (HttpContext.Session.GetString("UserID") != string.Empty)
                {
                    User user = UserOptions.GetUser(int.Parse(HttpContext.Session.GetString("UserID")));
                    ViewBag.username = user.username;
                }
                else
                {
                    ViewBag.username = string.Empty;
                }
            }
            else
            {
                ViewBag.username = string.Empty;
            }

            DBBooks.AddGenre(name);
            return View();
        }

        [HttpPost]
        public IActionResult DeleteGenre(int id)
        {
            if (HttpContext.Session.GetString("UserID") != null)
            {
                if (HttpContext.Session.GetString("UserID") != string.Empty)
                {
                    User user = UserOptions.GetUser(int.Parse(HttpContext.Session.GetString("UserID")));
                    ViewBag.username = user.username;
                }
                else
                {
                    ViewBag.username = string.Empty;
                }
            }
            else
            {
                ViewBag.username = string.Empty;
            }

            if (DBBooks.products.Where(p => p.genre_id == id).Count() == 0)
            {
                DBBooks.DeleteGenre(id);
            }

            ViewBag.Genres = DBBooks.genres;
            return View();
        }


        [HttpGet]
        public IActionResult DeleteGenre()
        {
            if (HttpContext.Session.GetString("UserID") != null)
            {
                if (HttpContext.Session.GetString("UserID") != string.Empty)
                {
                    User user = UserOptions.GetUser(int.Parse(HttpContext.Session.GetString("UserID")));
                    ViewBag.username = user.username;
                }
                else
                {
                    ViewBag.username = string.Empty;
                }
            }
            else
            {
                ViewBag.username = string.Empty;
            }


            ViewBag.Genres = DBBooks.genres;
            return View();
        }









    }
}
