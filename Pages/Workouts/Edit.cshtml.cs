using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkoutLibraryAndTracker.Models;

namespace WorkoutLibraryAndTracker.Pages.Workouts
{
    public class EditModel : PageModel
    {
        private readonly WorkoutLibraryAndTracker.Data.WorkoutDbContext _context;

        public EditModel(WorkoutLibraryAndTracker.Data.WorkoutDbContext context)
        {
            _context = context;
        }

        // Holds the dropdown list of categories
        public SelectList CategorySelectList { get; set; }

        [BindProperty]
        public Workout Workout { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Load the workout from the database
            var workout = await _context.Workouts
                // Optionally include Category if you want immediate access to its name, etc.
                .Include(w => w.Category)
                .FirstOrDefaultAsync(m => m.WorkoutId == id);

            if (workout == null)
            {
                return NotFound();
            }

            Workout = workout;

            // Build a SelectList for categories, selecting the workout’s current CategoryId by default
            var categories = await _context.Categories.ToListAsync();
            CategorySelectList = new SelectList(categories, "CategoryId", "Name", Workout.CategoryId);

            return Page();
        }

        // Handles the form submission for editing an existing Workout
        public async Task<IActionResult> OnPostAsync()
        {
            // If validation fails (e.g., required fields are missing), redisplay form with Category dropdown
            if (!ModelState.IsValid)
            {
                // Reload the category list
                var categories = await _context.Categories.ToListAsync();
                CategorySelectList = new SelectList(categories, "CategoryId", "Name", Workout.CategoryId);

                return Page();
            }

            // Mark the Workout entity as modified so EF will update it
            _context.Attach(Workout).State = EntityState.Modified;

            try
            {
                // Save changes to the database
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkoutExists(Workout.WorkoutId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // On success, redirect back to the Index page
            return RedirectToPage("./Index");
        }

        private bool WorkoutExists(int id)
        {
            return _context.Workouts.Any(e => e.WorkoutId == id);
        }
    }
}
