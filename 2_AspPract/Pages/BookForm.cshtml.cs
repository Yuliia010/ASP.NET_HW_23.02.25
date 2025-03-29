using _2_AspPract.Abstract;
using _2_AspPract.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _2_AspPract.Pages
{
    public class BookFormModel : PageModel
    {

        [BindProperty]
        public BookDTO Book { get; set; }
        public List<BookDTO> Books { get; set; }

        private readonly IBookService _bookService;
        public BookFormModel(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task OnGetAsync()
        {
            Books = await _bookService.GetAllAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _bookService.AddAsync(Book);
            return RedirectToPage("/BookForm");
            

        }
    }
}
