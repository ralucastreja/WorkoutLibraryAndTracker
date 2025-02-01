using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WorkoutLibraryAndTracker.Models;

namespace WorkoutLibraryAndTracker.Pages.WorkoutLogs
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
        ViewData["WorkoutId"] = new SelectList(_context.Workouts, "WorkoutId", "WorkoutId");
            return Page();
        }

        [BindProperty]
        public WorkoutLog WorkoutLog { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.WorkoutLogs.Add(WorkoutLog);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
