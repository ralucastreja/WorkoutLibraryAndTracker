using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkoutLibraryAndTracker.Models;

namespace WorkoutLibraryAndTracker.Pages.Workouts
{
    public class CreateModel : PageModel
    {
        private readonly WorkoutLibraryAndTracker.Data.WorkoutDbContext _context;

        public CreateModel(WorkoutLibraryAndTracker.Data.WorkoutDbContext context)
        {
            _context = context;
        }

        // Holds the dropdown list of categories
        public SelectList CategorySelectList { get; set; }

        [BindProperty]
        public Workout Workout { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            CategorySelectList = new SelectList(categories, "CategoryId", "Name");

            return Page();
        }

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // If the model state is invalid, reload the categories so the dropdown is available again
                var categories = await _context.Categories.ToListAsync();
                CategorySelectList = new SelectList(categories, "CategoryId", "Name");
                return Page();
            }

            _context.Workouts.Add(Workout);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
