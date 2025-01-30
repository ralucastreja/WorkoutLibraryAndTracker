using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkoutLibraryAndTracker.Data;
using WorkoutLibraryAndTracker.Models;

namespace WorkoutLibraryAndTracker.Pages.Workouts
    {
    public class CreateModel : PageModel
        {
        private readonly WorkoutDbContext _context;

        public CreateModel(WorkoutDbContext context)
            {
            _context = context;
            }

        // Holds the dropdown list of categories
        public SelectList CategorySelectList { get; set; }

        // The Workout we are creating
        [BindProperty]
        public Workout Workout { get; set; } = default!;

        // The WorkoutLog we are creating (for the newly created Workout)
        [BindProperty]
        public WorkoutLog WorkoutLog { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
            {
            var categories = await _context.Categories.ToListAsync();
            CategorySelectList = new SelectList(categories, "CategoryId", "Name");

            return Page();
            }

        public async Task<IActionResult> OnPostAsync()
            {
            if (!ModelState.IsValid)
                {
                var categories = await _context.Categories.ToListAsync();
                CategorySelectList = new SelectList(categories, "CategoryId", "Name");
                return Page();
                }

            // Ensure a valid category is selected
            if (Workout.CategoryId == 0)
                {
                ModelState.AddModelError("Workout.CategoryId", "You must select a category.");
                var categories = await _context.Categories.ToListAsync();
                CategorySelectList = new SelectList(categories, "CategoryId", "Name");
                return Page();
                }

            // 1) Create the Workout
            _context.Workouts.Add(Workout);
            await _context.SaveChangesAsync();

            // Ensure WorkoutLog is initialized
            if (WorkoutLog == null)
                {
                WorkoutLog = new WorkoutLog();
                }

            // 2) Create a WorkoutLog referencing the new Workout
            WorkoutLog.WorkoutId = Workout.WorkoutId;
            _context.WorkoutLogs.Add(WorkoutLog);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
            }
        }
    }
