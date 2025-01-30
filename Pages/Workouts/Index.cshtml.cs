using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WorkoutLibraryAndTracker.Models;

namespace WorkoutLibraryAndTracker.Pages.Workouts
    {
    public class IndexModel : PageModel
    {
        private readonly WorkoutLibraryAndTracker.Data.WorkoutDbContext _context;

        public IndexModel(WorkoutLibraryAndTracker.Data.WorkoutDbContext context)
        {
            _context = context;
        }

        public IList<Workout> Workout { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Workout = await _context.Workouts
                .Include(w => w.Category)
                .ToListAsync();
        }
    }
}
