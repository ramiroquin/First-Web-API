using FirstAPI.Data;
using FirstAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        private readonly BooksDb _context;
        public BooksController( BooksDb context) 
        {
            // Inyeccion de dependencias
            _context = context;
        }

        //GET: api/books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return await _context.Books.ToListAsync();
        }

        //Get: api/books/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook (int? id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }
            return book;
        }
        //Post: api/books
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetBook", new {id = book.Id}, book);
        }
        //Put: api/books/2
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int? id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }
            var bookInDb = await _context.Books.FindAsync(id);

            if (bookInDb == null)
            {
                return NotFound();
            }

            bookInDb.Title = book.Title;
            bookInDb.Author = book.Author;
            bookInDb.IsAvailable = book.IsAvailable;

            await _context.SaveChangesAsync();

            return NoContent();
        }
        //Delete: api/books/2
        [HttpDelete("{id}")]
        public async Task<ActionResult<Book>> DeleteBook(int? id)
        {
            var book = await _context.Books.FindAsync (id);
            if (book == null)
            {
                return NotFound();                
            }

            _context.Books.Remove(book);

            await _context.SaveChangesAsync();

            return book;
        }
    }
}
