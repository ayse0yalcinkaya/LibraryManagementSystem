using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task AddAsync(Book book)  // kitap ekleme
        {
            _context.Books.Add(book);   // kitap nesnesini tabloya
            await _context.SaveChangesAsync();  // değişiklikleri veritabanına
        }

        public async Task UpdateAsync(Book book) //kitap güncelleme aksiyonu
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        //silme aksiyonu
        public async Task DeleteAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }


    }
}
