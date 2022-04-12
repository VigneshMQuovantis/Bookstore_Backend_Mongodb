using CommonLayer.BookModels;
using MongoDB.Bson;
using MongoDB.Driver;
using RepositoryLayer.DatabaseConfig;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class BookRL : IBookRL
    {
        private readonly IMongoCollection<BookEntities> bookEntities;

        public BookRL(IBookstoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            bookEntities = database.GetCollection<BookEntities>(settings.BooksCollectionName);
        }

        public BookResponseModel CreateBookDetails(AddBookModel model)
        {
            try
            {
                BookEntities entities = new()
                {
                    BookId = ObjectId.GenerateNewId().ToString(),
                    BookName = model.BookName,
                    BookAuthor = model.BookAuthor,
                    OriginalPrice = model.OriginalPrice,
                    DiscountPrice = model.DiscountPrice,
                    BookQuantity = model.BookQuantity,
                    BookDetails = model.BookDetails,
                    TotalRatings = model.TotalRatings,
                    NoOfPeopleRated = model.NoOfPeopleRated,
                    AddedAt = DateTime.Now.ToString()
                };

                bookEntities.InsertOne(entities);

                BookResponseModel responseModel = new()
                {
                    BookId = entities.BookId,
                    BookName = entities.BookName,
                    BookAuthor = entities.BookAuthor,
                    BookDetails = entities.BookDetails,
                    OriginalPrice = entities.OriginalPrice,
                    DiscountPrice = entities.DiscountPrice,
                    BookQuantity = entities.BookQuantity,
                    TotalRatings = entities.TotalRatings,
                    NoOfPeopleRated = entities.NoOfPeopleRated,
                    AddedAt = entities.AddedAt,
                };
                return responseModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<BookResponseModel> GetAllBook()
        {
            try
            {
                List<BookResponseModel> allBooks = new List<BookResponseModel>();
                var validateBooks = bookEntities.Find(e=>true).ToList();
                foreach(var book in validateBooks)
                {
                    allBooks.Add(new BookResponseModel()
                    {
                        BookId = book.BookId.ToString(),
                        BookName = book.BookName,
                        BookAuthor = book.BookAuthor,
                        OriginalPrice = book.OriginalPrice,
                        DiscountPrice = book.DiscountPrice,
                        BookQuantity = book.BookQuantity,
                        BookDetails = book.BookDetails,
                        TotalRatings = book.TotalRatings,
                        NoOfPeopleRated = book.NoOfPeopleRated,
                        AddedAt = book.AddedAt,
                    });
                }
                return allBooks;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BookResponseModel GetBookWithBookId(string bookId)
        {
            try
            {
                var validateBooks = bookEntities.Find(e => e.BookId == bookId).FirstOrDefault();
                if(validateBooks != null)
                {
                    BookResponseModel responseModel = new()
                    {
                        BookId = validateBooks.BookId.ToString(),
                        BookName = validateBooks.BookName,
                        BookAuthor = validateBooks.BookAuthor,
                        OriginalPrice = validateBooks.OriginalPrice,
                        DiscountPrice = validateBooks.DiscountPrice,
                        BookQuantity = validateBooks.BookQuantity,
                        BookDetails = validateBooks.BookDetails,
                        TotalRatings = validateBooks.TotalRatings,
                        NoOfPeopleRated = validateBooks.NoOfPeopleRated,
                        AddedAt = validateBooks.AddedAt,
                    };
                    return responseModel;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BookResponseModel UpdateBookDetails(string bookId, UpdateBookModel model)
        {
            try
            {
                var validateBook = bookEntities.Find(e => e.BookId == bookId).FirstOrDefault();
                if(validateBook != null)
                {
                    BookEntities entities = new()
                    {
                        BookId = validateBook.BookId,
                        BookName = model.BookName,
                        BookAuthor = model.BookAuthor,
                        OriginalPrice = model.OriginalPrice,
                        DiscountPrice = model.DiscountPrice,
                        BookQuantity = model.BookQuantity,
                        BookDetails = model.BookDetails,
                        TotalRatings = validateBook.TotalRatings,
                        NoOfPeopleRated = validateBook.NoOfPeopleRated,
                        AddedAt = validateBook.AddedAt
                    };

                    var updateBook = bookEntities.ReplaceOne(e => e.BookId == entities.BookId,entities);

                    BookResponseModel responseModel = new()
                    {
                        BookId = entities.BookId,
                        BookName = entities.BookName,
                        BookAuthor = entities.BookAuthor,
                        OriginalPrice = entities.OriginalPrice,
                        DiscountPrice = entities.DiscountPrice,
                        BookQuantity = entities.BookQuantity,
                        BookDetails = entities.BookDetails,
                        TotalRatings = entities.TotalRatings,
                        NoOfPeopleRated = entities.NoOfPeopleRated,
                        AddedAt = entities.AddedAt
                    };
                    return responseModel;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
