using BookStore.Data.Entities;
using BookStore.Data.Repositories;
using BookStore.WebApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebApplication.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public IActionResult Index()
        {
            var books = _bookRepository.GetAllBooks();
            return View(books);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Book model)
        {
            _bookRepository.AddBook(model);

            return RedirectToAction("Index");
        }

        [Route("book/{id}")]
        public IActionResult Details(int id)
        {
            var book = _bookRepository.GetBookDetails(id);
            return View(book);
        }
    }
}
