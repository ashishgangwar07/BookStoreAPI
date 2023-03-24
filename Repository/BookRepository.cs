using BookStore.API.Data;
using BookStore.API.Models;
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
    }
}
