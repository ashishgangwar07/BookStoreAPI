using BookStore.API.Data;
using BookStore.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreDbContext _context;

        public BookRepository(BookStoreDbContext context) {
            _context = context;
        }
        public async Task<List<BookModel>> GetAllBooksAsync(){
            //converting books type data to bookModel
            var records= await _context.Books.Select(x => new BookModel()
            {
                Id= x.Id,
                Title= x.Title,
                Description = x.Description,
            }).ToListAsync();
            return records;
        }

        public async Task<BookModel> GetBookByIdAsync(int bookId)
        {
            var record= await _context.Books.Where(x => x.Id==bookId).Select(x => new BookModel()
            {
                Id = x.Id,
                Title= x.Title,
                Description = x.Description,
            }).FirstOrDefaultAsync();
            return record;
        }

        public async Task<int> AddBookAsync(BookModel bookModel)
        {//converting data from BookModel to Book
            var book = new Books()
            {
                Title = bookModel.Title,
                Description = bookModel.Description,
            };
            _context.Books.Add(book);
           await _context.SaveChangesAsync();
            return book.Id;
        }
        public async Task UpdateBookAsync(int bookId, BookModel bookModel)
        {//we are getting book deatils and updating(2 times db calls)
            //var book = await _context.Books.FindAsync(bookId);
            //if(book != null)
            //{
            //    book.Title = bookModel.Title;
            //    book.Description = bookModel.Description;
            //   await  _context.SaveChangesAsync();
            //}
            //
            var book = new Books()
            { Id = bookId,
                Title = bookModel.Title,
                Description = bookModel.Description,
            };
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateBookPatchAsync(int bookId, JsonPatchDocument bookModel)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book != null)
            {
                bookModel.ApplyTo(book);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteBookAsync(int bookId)
        {
            var book = new Books()
            { Id = bookId };
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
    }
}
