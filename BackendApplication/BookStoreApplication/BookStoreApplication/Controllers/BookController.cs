using CommonLayer.BookModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using RepositoryLayer.Interfaces;

namespace BookStoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRL bookRL;

        public BookController(IBookRL bookRL)
        {
            this.bookRL = bookRL;
        }

        [HttpPost]
        public IActionResult CreateBookDetails(AddBookModel model)
        {
            try
            {
                if (model == null)
                {
                    return NotFound(new { Success = false, message = "Not able to create book" });
                }
                BookResponseModel book = bookRL.CreateBookDetails(model);
                return Ok(new { Success = true, message = "Book Created Successfully ", book });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAllBook()
        {
            try
            {
                IEnumerable<BookResponseModel> book = bookRL.GetAllBook();
                if (book == null)
                {
                    return NotFound(new { Success = false, message = "Cannot retrive books" });
                }

                return Ok(new { Success = true, message = "Retrived All Book ", book });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("{bookId}")]
        public IActionResult GetBookWithBookId(string bookId)
        {
            try
            {
                BookResponseModel allBooks = bookRL.GetBookWithBookId(bookId);
                if (allBooks == null)
                {
                    return NotFound(new { Success = false, message = "Invalid BookId" });
                }

                return Ok(new { Success = true, message = "Retrived Book BookId ", allBooks });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut("{bookId}")]
        public IActionResult UpdateBookDetails(string bookId, UpdateBookModel model)
        {
            try
            {
                BookResponseModel book = bookRL.UpdateBookDetails(bookId, model);
                if (book == null)
                {
                    return NotFound(new { Success = false, message = "Invalid BookId to update" });
                }

                return Ok(new { Success = true, message = "Book Updated Successfully ", book });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
