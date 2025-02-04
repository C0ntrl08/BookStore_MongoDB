using BookStore.Models;
using BookStore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // Declare that the controller's actions support a response content type of application/json
    [Produces("application/json")]
    public class BooksController : ControllerBase
    {
        private readonly BooksService _booksService;

        public BooksController(BooksService bookService)
        {
            _booksService = bookService;
        }

        [HttpGet]
        public async Task<List<Book>> GetAllBooks()
        {
            return await _booksService.GetAsync();
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Book>> GetBookById(string id)
        {
            var book = await _booksService.GetAsyncById(id);

            if (book is null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        /// <summary>
        /// Creates a new book with the given parameters
        /// </summary>
        /// <param name="newBook"></param>
        /// <response code="201">The item has been successfully created</response>
        [HttpPost]
        public async Task<IActionResult> PostBook(Book newBook)
        {
            await _booksService.CreateAsync(newBook);

            return CreatedAtAction(nameof(GetBookById), new { id = newBook.Id }, newBook);
        }

        /// <summary>
        /// Updates a book with the given Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedBook"></param>
        /// <response code="204">The item has been successfully updated</response>
        /// <response code="404">The item was not found</response>
        [HttpPut("{id:length(24)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update(string id, Book updatedBook)
        {
            var book = await _booksService.GetAsyncById(id);

            if (book is null)
            {
                return NotFound();
            }

            updatedBook.Id = book.Id;

            await _booksService.UpdateAsync(id, updatedBook);

            // HTTP - 204
            return NoContent();
        }

        //TODO - HTTPPatch

        /// <summary>
        /// Deletes a book from the database with the given Id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204">The item has been successfully deleted</response>
        /// <response code="404">The item was not found</response>
        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(string id)
        {
            var book = _booksService.GetAsyncById(id);

            if (book is null)
            {
                return NotFound();
            }

            await _booksService.RemoveAsync(id);

            // HTTP - 204
            return NoContent();
        }
    }
}
