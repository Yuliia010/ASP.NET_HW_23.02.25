using _2_AspPract.Models;

namespace _2_AspPract.Abstract
{
    public interface IOpenLibraryService
    {
        public Task<List<BookDTO>> GetBookByNameAsync(string query);
        public List<BookDTO> ExtractBooks(string json);
    }
}
