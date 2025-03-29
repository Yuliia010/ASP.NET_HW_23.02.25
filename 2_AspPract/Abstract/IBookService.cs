using _2_AspPract.Models;

namespace _2_AspPract.Abstract
{
    public interface IBookService
    {
        Task<List<BookDTO>> GetAllAsync();
        Task AddAsync(BookDTO book);
        Task UpdateAsync(BookDTO book);
        Task DeleteAsync(Guid id);
    }
}
