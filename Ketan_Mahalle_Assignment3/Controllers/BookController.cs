using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookController : Controller
    {
        public string URI = "https://localhost:8081";
        public string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        public string DatabaseName = "Library";
        public string ContainerName = "Book";

        public Container container;

        private Container GetContainer()
        {
            CosmosClient cosmosClient = new CosmosClient(URI, PrimaryKey);
            Database database = cosmosClient.GetDatabase(DatabaseName);
            Container container = database.GetContainer(ContainerName);
            return container;
        }
        public BookController()
        {
            container = GetContainer();

        }
        [HttpPost]

        public async Task<Models.Bookmodel> AddBook(Models.Bookmodel bookmodel)
        {

            Entities.BookEntity book1 = new Entities.BookEntity();
            book1.UId = bookmodel.UId;
            book1.Title = bookmodel.Title;
            book1.Author = bookmodel.Author;
            book1.ISBN = bookmodel.ISBN;
            book1.PublishedDate = bookmodel.PublishedDate;
            book1.IsIssued = bookmodel.IsIssued;



            book1.Id = Guid.NewGuid().ToString();
            book1.UId = book1.Id;
            book1.DocumentType = "Book";
            book1.CreatedBy = "Ketan";
            book1.CreatedOn = DateTime.Now;
            book1.UpdatedBy = "Ketan";
            book1.UpdatedOn = DateTime.Now;
            book1.Version = 1;
            book1.Active = true;
            book1.Archived = false;


            Entities.BookEntity response = await container.CreateItemAsync(book1);


            Models.Bookmodel responseModel = new Models.Bookmodel();
            responseModel.UId = response.UId;
            responseModel.Title = response.Title;
            responseModel.Author = response.Author;
            responseModel.ISBN = response.ISBN;
            responseModel.IsIssued = response.IsIssued;
            return responseModel;

        }
        [HttpGet]
        public async Task<Models.Bookmodel> GetBookByUID(String uId)
        {
            var book = container.GetItemLinqQueryable<Models.Bookmodel>(true).Where(q => q.UId == uId).FirstOrDefault();
            return book;
        }
        [HttpGet]
        public async Task<Models.Bookmodel> GetBookByTitle(String title)
        {
            var book = container.GetItemLinqQueryable<Models.Bookmodel>(true).Where(q => q.Title == title).FirstOrDefault();
            return book;
        }
        [HttpGet]
        public async Task<List<Models.Bookmodel>> GetAllBooks()
        {
            var book = container.GetItemLinqQueryable<Models.Bookmodel>(true).ToList();
            return book;
        }
        [HttpGet]
        public async Task<List<Models.Bookmodel>> GetAvailableBooks()
        {
            var book = container.GetItemLinqQueryable<Models.Bookmodel>(true).Where(q => q.IsIssued == false).ToList();
            return book;
        }
        [HttpGet]
        public async Task<List<Models.Bookmodel>> GetIssuedBooks()
        {
            var book = container.GetItemLinqQueryable<Models.Bookmodel>(true).Where(q => q.IsIssued == true).ToList();
            return book;
        }
        [HttpPost]
        public async Task<Models.Bookmodel> UpdateBook(Models.Bookmodel book)
        {

            var existingBook = container.GetItemLinqQueryable<Entities.BookEntity>(true).Where(q => q.UId == book.UId && q.Active == true && q.Archived == false).FirstOrDefault();

            existingBook.Archived = true;
            existingBook.Active = false;
            await container.ReplaceItemAsync(existingBook, existingBook.Id);


            existingBook.Id = Guid.NewGuid().ToString();
            existingBook.UpdatedBy = "Ketan";
            existingBook.UpdatedOn = DateTime.Now;
            existingBook.Version = existingBook.Version + 1;
            existingBook.Active = true;
            existingBook.Archived = false;

            existingBook.Author = book.Author;
            existingBook.Title = book.Title;
            existingBook.ISBN = book.ISBN;
            existingBook.IsIssued = book.IsIssued;
            existingBook.PublishedDate = book.PublishedDate;


            existingBook = await container.CreateItemAsync(existingBook);

            //6. Return

            Models.Bookmodel reponse = new Models.Bookmodel();
            reponse.UId = existingBook.UId;
            reponse.Author = existingBook.Author;
            reponse.Title = existingBook.Title;
            reponse.ISBN = existingBook.ISBN;
            reponse.IsIssued = existingBook.IsIssued;
            reponse.PublishedDate = existingBook.PublishedDate;
            return reponse;

        }










    }
}
