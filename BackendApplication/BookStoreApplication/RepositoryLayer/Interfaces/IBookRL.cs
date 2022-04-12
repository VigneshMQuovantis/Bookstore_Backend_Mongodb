using CommonLayer.BookModels;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IBookRL
    {
        BookResponseModel CreateBookDetails(AddBookModel model);
        IEnumerable<BookResponseModel> GetAllBook();
        BookResponseModel GetBookWithBookId(string bookId);
        BookResponseModel UpdateBookDetails(string bookId, UpdateBookModel model);
    }
}
