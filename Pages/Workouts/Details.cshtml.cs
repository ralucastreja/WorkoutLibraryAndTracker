using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WorkoutLibraryAndTracker.Models;

namespace WorkoutLibraryAndTracker.Pages.Workouts
    {
    public class DetailsModel : PageModel
        {
        private readonly WorkoutLibraryAndTracker.Data.WorkoutDbContext _context;

        public DetailsModel(WorkoutLibraryAndTracker.Data.WorkoutDbContext context)
            {
            _context = context;
            }

        public Workout Workout { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
            {
            if (id == null)
                {
                return NotFound();
                }

            Workout = await _context.Workouts
                .Include(w => w.Category)  // Ensure Category is loaded
                .Include(w => w.WorkoutEquipments)  // Load WorkoutEquipment relations
                .ThenInclude(we => we.Equipment)  // Load Equipment details
                .FirstOrDefaultAsync(m => m.WorkoutId == id);

            if (Workout == null)
                {
                return NotFound();
                }

            return Page();
            }
        }
    }
