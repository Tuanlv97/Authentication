using BookStore.WebApplication.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace BookStore.WebApplication.Services
{
    public interface IBookStoreAPIService
    {
        Task<HttpResponseMessage> GetBooks();
        Task<HttpResponseMessage> BookDetails(int id);
        Task<HttpResponseMessage> AddBook(BookModel model);
    }
}
