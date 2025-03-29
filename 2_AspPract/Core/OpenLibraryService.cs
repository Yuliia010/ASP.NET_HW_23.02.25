using _2_AspPract.Abstract;
using _2_AspPract.Models;
using AspSecond.DAL.Entities;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace _2_AspPract.Core
{
    public class OpenLibraryService : IOpenLibraryService
    {
        private readonly HttpClient _httpClient;

        public OpenLibraryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<BookDTO>> GetBookByNameAsync(string query)
        {
            if (query.IsNullOrEmpty()) 
            { 
                return new List<BookDTO>(); 
            }
            string input = string.Join("+", query.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)); ;
            
            var response = await _httpClient.GetAsync($"/search.json?q={input}&format=json&jscmd=data");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var data = ExtractBooks(content);
            return data;
        }

        public List<BookDTO> ExtractBooks(string json)
        {
            var books = new List<BookDTO>();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var document = JsonDocument.Parse(json);

            var root = document.RootElement;
            if (!root.TryGetProperty("docs", out var contentArray)
                || contentArray.ValueKind != JsonValueKind.Array
                || contentArray.GetArrayLength() == 0)
            {
                return new List<BookDTO>();
            }

            for (int i = 0; i < Math.Min(10, contentArray.GetArrayLength()); i++)
            {
                var firstContent = contentArray[i];

                var author_name = firstContent.TryGetProperty("author_name", out var auth_name) && auth_name.GetArrayLength() > 0
                                  ? auth_name[0].GetString() ?? string.Empty
                                  : string.Empty;

                DateTime? publish_year = firstContent.TryGetProperty("first_publish_year", out var year) && year.ValueKind == JsonValueKind.Number
                                         ? new DateTime(year.GetInt32(), 1, 1)
                                         : null;

                var title = firstContent.TryGetProperty("title", out var bookTitle)
                             ? bookTitle.GetString() ?? string.Empty
                             : string.Empty;

                var result = new BookDTO()
                {
                    Title = title,
                    Author_name = author_name,
                    First_publish_year = publish_year,
                };
                books.Add(result);
            }

            return books;
        }
    }
}
