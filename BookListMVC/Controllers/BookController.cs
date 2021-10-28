using BookListMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookListMVC.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext DB;

        [BindProperty]
        public Book book { get; set; }

        public BookController(ApplicationDbContext Database)
        {
            this.DB = Database;
        }

        public IActionResult Index()
        {
            IEnumerable<Book> BooksList = DB.Books.ToList();

            return View(BooksList);
        }

        public IActionResult Create() {
            return View(this.book);
        }

        [ValidateAntiForgeryToken]
        public IActionResult Store() {

            if (ModelState.IsValid)
            {
                DB.Books.Add(book);
                DB.SaveChanges();
            }
            else {
                return RedirectToAction();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id) {

            this.book = this.DB.Books.Find(id);

            return View(this.book);
        }

        [ValidateAntiForgeryToken]
        public IActionResult Update() {

            if (ModelState.IsValid)
            {
                if (this.DB.Books.Find(this.book.Id) != null)
                {
                    this.DB.Books.Update(book);

                    this.DB.SaveChanges();

                }
                else {
                    return  NotFound();
                }
            }
            else {

                return View();
            
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {

            this.book = this.DB.Books.Find(id);

            return View(this.book);
        }

        [ValidateAntiForgeryToken]
        public IActionResult Destroy(int id) {

            Book deleteBook = this.DB.Books.Find(this.book.Id);

            if (deleteBook != null)
            {
                this.DB.Books.Remove(deleteBook);

                this.DB.SaveChanges();

            }
            else
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            this.book = new Book();

            if (id == null)
            {
                //create
                return View(this.book);

            }
            //update
            this.book = this.DB.Books.Find(id);

            if (this.book == null)
            {
                return NotFound();
            }

            return View(this.book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {

            if (ModelState.IsValid)
            {
                if (this.book.Id == 0)
                {
                    this.DB.Books.Add(book);

                    this.DB.SaveChanges();

                }
                else
                {
                    this.DB.Books.Update(book);

                    this.DB.SaveChanges();
                }
            }
            else
            {

                return View();

            }

            return RedirectToAction("Index");
        }
    }
}
