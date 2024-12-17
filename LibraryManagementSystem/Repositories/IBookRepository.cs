using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book> GetByIdAsync(int id);

        Task AddAsync(Book book);  // test verisi için yeni metod

        Task UpdateAsync(Book book); //guncelleme
        Task DeleteAsync(int id); //silme
    }
}
