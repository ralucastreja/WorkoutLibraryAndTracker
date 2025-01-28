using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WorkoutLibraryAndTracker.Data;
using WorkoutLibraryAndTracker.Models;

namespace WorkoutLibraryAndTracker.Pages.WorkoutLogs
{
    public class DeleteModel : PageModel
    {
        private readonly WorkoutLibraryAndTracker.Data.WorkoutDbContext _context;

        public DeleteModel(WorkoutLibraryAndTracker.Data.WorkoutDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public WorkoutLog WorkoutLog { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workoutlog = await _context.WorkoutLogs.FirstOrDefaultAsync(m => m.WorkoutLogId == id);

            if (workoutlog == null)
            {
                return NotFound();
            }
            else
            {
                WorkoutLog = workoutlog;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workoutlog = await _context.WorkoutLogs.FindAsync(id);
            if (workoutlog != null)
            {
                WorkoutLog = workoutlog;
                _context.WorkoutLogs.Remove(WorkoutLog);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
