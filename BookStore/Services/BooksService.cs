using BookStore.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Runtime.CompilerServices;

namespace BookStore.Services
{
    public class BooksService
    {
        private readonly IMongoCollection<Book> _booksCollection;

        public BooksService(IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings)
        {
            // Reads the server instance for running database operations
            var mongoClient = new MongoClient(bookStoreDatabaseSettings.Value.ConnectionString);
            // Represents the Mongo database for running operations
            var mongoDatabase = mongoClient.GetDatabase(bookStoreDatabaseSettings.Value.DatabaseName);

            _booksCollection = mongoDatabase.GetCollection<Book>(bookStoreDatabaseSettings.Value.BooksCollectionName);
        }

        public async Task<List<Book>> GetAsync()
        {
            return await _booksCollection.Find(Book => true).ToListAsync();
        }

        public async Task<Book?> GetAsyncById(string id)
        {
            return await _booksCollection.Find(book => book.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Book newBook)
        {
            await _booksCollection.InsertOneAsync(newBook);
        }

        public async Task UpdateAsync(string id, Book updatedBook)
        {
            await _booksCollection.ReplaceOneAsync(book => book.Id == id, updatedBook);
        }

        public async Task RemoveAsync(string id)
        {
            await _booksCollection.DeleteOneAsync(book => book.Id == id);
        }
    }
}
