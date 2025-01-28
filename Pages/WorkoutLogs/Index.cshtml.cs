using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WorkoutLibraryAndTracker.Models;

namespace WorkoutLibraryAndTracker.Pages.WorkoutLogs
    {
    public class IndexModel : PageModel
    {
        private readonly WorkoutLibraryAndTracker.Data.WorkoutDbContext _context;

        public IndexModel(WorkoutLibraryAndTracker.Data.WorkoutDbContext context)
        {
            _context = context;
        }

        public IList<WorkoutLog> WorkoutLog { get;set; } = default!;

        public async Task OnGetAsync()
        {
            WorkoutLog = await _context.WorkoutLogs
                .Include(w => w.Workout).ToListAsync();
        }
    }
}
