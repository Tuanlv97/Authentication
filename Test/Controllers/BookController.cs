using BookStore.WebApplication.Models;
using BookStore.WebApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BookStore.WebApplication.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookStoreAPIService _bookStoreAPIService;
        public BookController(IBookStoreAPIService bookStoreAPIService)
        {
            _bookStoreAPIService = bookStoreAPIService;
        }
      // [Authorize(Roles = "Viewers, Administrator")]
        public async Task<IActionResult> Index()
        {
            var books = new List<BookModel>();

            var response = await _bookStoreAPIService.GetBooks();

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return Unauthorized();
            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync();

                books = JsonConvert.DeserializeObject<List<BookModel>>(json);
                return View(books);
            }
            return Problem();
           
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookModel model)
        {
            var response = await _bookStoreAPIService.AddBook(model);

            using (var client = new HttpClient())
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    return Unauthorized();

                if(response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction("Index");
                }
            return Problem();
            
        }

        //[Route("book/{id}")]
        //public IActionResult Details(int id)
        //{
        //    var book = _bookRepository.GetBookDetails(id);
        //    return View(book);
        //}
    }
}
