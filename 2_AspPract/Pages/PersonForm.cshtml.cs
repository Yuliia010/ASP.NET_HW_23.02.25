using _2_AspPract.Abstract;
using _2_AspPract.Models;
using AspSecond.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _2_AspPract.Pages
{
    public class PersonModel : PageModel
    {
      
        [BindProperty]
        public PersonDto Person { get; set; }
        public List<PersonDto> Persons { get; set; }

        private readonly IPersonService _personService;
        public PersonModel(IPersonService personService)
        {
            _personService = personService;
        }

        public async Task OnGetAsync()
        {
            Persons = await _personService.GetAllAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _personService.AddAsync(Person);
            return RedirectToPage("/PersonForm");
           // Persons = await _personService.GetAllAsync();
            //Person = new PersonDto();

        }
    }
}
