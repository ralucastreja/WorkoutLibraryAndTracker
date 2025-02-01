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

        public SelectList CategorySelectList { get; set; }

        [BindProperty]
        public Workout Workout { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workout = await _context.Workouts
                .Include(w => w.Category)
                .FirstOrDefaultAsync(m => m.WorkoutId == id);

            if (workout == null)
            {
                return NotFound();
            }

            Workout = workout;

            var categories = await _context.Categories.ToListAsync();
            CategorySelectList = new SelectList(categories, "CategoryId", "Name", Workout.CategoryId);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var categories = await _context.Categories.ToListAsync();
                CategorySelectList = new SelectList(categories, "CategoryId", "Name", Workout.CategoryId);

                return Page();
            }

            _context.Attach(Workout).State = EntityState.Modified;

            try
            {
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

            return RedirectToPage("./Index");
        }

        private bool WorkoutExists(int id)
        {
            return _context.Workouts.Any(e => e.WorkoutId == id);
        }
    }
}
