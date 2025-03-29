using _2_AspPract.Abstract;
using _2_AspPract.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace _2_AspPract.Pages
{
    public class BookFormModel : PageModel
    {

        [BindProperty]
        public BookDTO Book { get; set; }

        [BindProperty]
        public string Title { get; set; }
        public List<BookDTO> Books { get; set; } = new List<BookDTO>();
        public List<BookDTO> BooksFromApi { get; set; } =  new List<BookDTO>();

        private readonly IBookService _bookService;
        private readonly IOpenLibraryService _openLibraryService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BookFormModel(IBookService bookService, IOpenLibraryService openLibraryService, IHttpContextAccessor httpContextAccessor)
        {
            _bookService = bookService;
            _openLibraryService = openLibraryService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task OnGetAsync()
        {
            Books = await _bookService.GetAllAsync();

            var savedTitle = _httpContextAccessor.HttpContext.Request.Cookies["SavedTitle"] ?? string.Empty;

            if (savedTitle.IsNullOrEmpty())
            {
                _httpContextAccessor.HttpContext.Response.Cookies.Delete("BooksFromApi");
                BooksFromApi = new List<BookDTO>();
            }
            else
            {
                var cookieValue = _httpContextAccessor.HttpContext.Request.Cookies["BooksFromApi"] ?? string.Empty;
                if (!string.IsNullOrEmpty(cookieValue))
                {
                    BooksFromApi = JsonSerializer.Deserialize<List<BookDTO>>(cookieValue) ?? new List<BookDTO>();
                }
            }

        }

        public async Task<IActionResult> OnPostFirstAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            var data = await _openLibraryService.GetBookByNameAsync(Title);
            if (data != null)
            {
                await SetBooksFromApiInfoCookieAsync(data);
            }
            Title = string.Empty;


            return RedirectToPage("/BookForm");

        }
        public async Task<IActionResult> OnPostSecondAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            await _bookService.AddAsync(Book);

            return RedirectToPage("/BookForm");

        }
        public async Task SetBooksFromApiInfoCookieAsync(List<BookDTO> booksLst)
        {
            string booksJson = JsonSerializer.Serialize(booksLst);

            await Task.Run(() =>
              _httpContextAccessor.HttpContext.Response.Cookies.Append("BooksFromApi", booksJson, new CookieOptions
              {
                  HttpOnly = true,
                  Secure = true,
                  Expires = DateTime.UtcNow.AddHours(1)
              })
            );
        }
    }
}
