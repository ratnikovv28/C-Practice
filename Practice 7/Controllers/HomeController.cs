using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Practice_7.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Practice_7.Controllers
{
    //public static class MemoryDB
    //{
    //    public static List<Song> Songs { get; set; } = new List<Song>()
    //    {
    //        new Song(){Id = 1, Name = "Drip too hard", Genre = "Rap", Year = 2018, Author = new Author(){ Pseudonym = "Lil Baby", Id = 1} }
    //    };
    //}

    public class HomeController : Controller
    {
        private SongsDBContext _db;

        public HomeController(SongsDBContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            // Готовим данные для представления
            ViewData["Title"] = "Песни";
            ViewModel mymodel = new ViewModel();
            mymodel.Songs = _db.Songs.Include(s => s.Author).ToList();
            mymodel.Authors = _db.Authors.ToList();
            //Передаем управление "Представлению"
            return View(mymodel);
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

        [HttpGet]
        public IActionResult CreateSong()
        {
            ViewBag.Authorss = new SelectList(_db.Authors, "Id", "Pseudonym");
            Song song = new Song();
            return View(song);
        }

        [HttpPost]
        public IActionResult CreateSong(Song song)
        {
            if (ModelState.IsValid)
            {
                _db.Songs.Add(song);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(song);
        }

        [HttpGet]
        public IActionResult CreateAuthor()
        {
            //ViewBag.Authorss = new SelectList(_db.Authors, "Id", "Pseudonym");
            Author author = new Author();
            return View(author);
        }

        [HttpPost]
        public IActionResult CreateAuthor(Author author)
        {
            if (ModelState.IsValid)
            {
                _db.Authors.Add(author);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(author);
        }


        [HttpGet]
        public IActionResult EditSong(int Id)
        {
            var song = _db.Songs.FirstOrDefault(s => s.Id == Id);
            ViewBag.Authorss = new SelectList(_db.Authors, "Id", "Pseudonym");
            return View(song);
        }

        [HttpPost]
        public IActionResult EditSong(Song song)
        {
            if (ModelState.IsValid)
            {
                _db.Songs.Update(song);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult DeleteSong(Song song)
        {
            _db.Songs.Attach(song);
            _db.Songs.Remove(song);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult DeleteAuthor(Author author)
        {
            _db.Authors.Attach(author);
            _db.Authors.Remove(author);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult ShowAuthor(int id)
        {
            var instr = _db.Authors.FirstOrDefault(a => a.Id == id);
            var mus = _db.Songs.Where(s => s.AuthorId == id);
            ViewData["Songs"] = mus;
            return View(instr);
        }

        [HttpGet]
        public IActionResult EditAuthor(int Id)
        {
            var author = _db.Authors.FirstOrDefault(a => a.Id == Id);
            ViewBag.Autho = new SelectList(_db.Authors, "Id", "Pseudonym");
            return View(author);
        }

        [HttpPost]
        public IActionResult EditAuthor(Author author)
        {
            if (ModelState.IsValid)
            {
                _db.Authors.Update(author);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
