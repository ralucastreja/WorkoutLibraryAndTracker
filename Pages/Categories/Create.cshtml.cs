using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WorkoutLibraryAndTracker.Models;

namespace WorkoutLibraryAndTracker.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly WorkoutLibraryAndTracker.Data.WorkoutDbContext _context;

        public CreateModel(WorkoutLibraryAndTracker.Data.WorkoutDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Category Category { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Categories.Add(Category);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
