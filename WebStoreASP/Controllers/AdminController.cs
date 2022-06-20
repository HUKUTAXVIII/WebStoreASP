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

                ViewBag.Categories = DBBooks.categories;
                ViewBag.Genres = DBBooks.genres;
                ViewBag.Authors = DBBooks.authors;
                ViewBag.Covers = DBBooks.covers;
                ViewBag.Publishers = DBBooks.publishers;

            return View();
        }

        [HttpGet]
        public IActionResult DeleteBook()
        {
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

            return View();
        }

        [HttpGet]
        public IActionResult AddAuthor()
        {

            return View();
        }

        [HttpPost]
        public IActionResult AddAuthor(string name)
        {
            DBBooks.AddAuthor(name);
            return View();
        }

        [HttpPost]
        public IActionResult DeleteAuthor(int id)
        {
            DBBooks.DeleteAuthor(id);
            ViewBag.Authors = DBBooks.authors;
            return View();
        }

        [HttpGet]
        public IActionResult DeleteAuthor()
        {
            ViewBag.Authors = DBBooks.authors;
            return View();
        }




        [HttpGet]
        public IActionResult AddPublisher()
        {

            return View();
        }

        [HttpPost]
        public IActionResult AddPublisher(string name)
        {
            DBBooks.AddPublisher(name);
            return View();
        }

        [HttpPost]
        public IActionResult DeletePublisher(int id)
        {
            DBBooks.DeletePublisher(id);
            ViewBag.Publishers = DBBooks.publishers;
            return View();
        }


        [HttpGet]
        public IActionResult DeletePublisher()
        {
            ViewBag.Publishers = DBBooks.publishers;
            return View();
        }




        [HttpGet]
        public IActionResult AddCategory()
        {

            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(string name)
        {
            DBBooks.AddCategory(name);
            return View();
        }

        [HttpPost]
        public IActionResult DeleteCategory(int id)
        {
            DBBooks.DeleteCategory(id);
            ViewBag.Categories = DBBooks.categories;
            return View();
        }


        [HttpGet]
        public IActionResult DeleteCategory()
        {
            ViewBag.Categories = DBBooks.categories;
            return View();
        }



        [HttpGet]
        public IActionResult AddGenre()
        {

            return View();
        }

        [HttpPost]
        public IActionResult AddGenre(string name)
        {
            DBBooks.AddGenre(name);
            return View();
        }

        [HttpPost]
        public IActionResult DeleteGenre(int id)
        {
            DBBooks.DeleteGenre(id);
            ViewBag.Genres = DBBooks.genres;
            return View();
        }


        [HttpGet]
        public IActionResult DeleteGenre()
        {
            ViewBag.Genres = DBBooks.genres;
            return View();
        }









    }
}
