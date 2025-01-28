using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkoutLibraryAndTracker.Data;
using WorkoutLibraryAndTracker.Models;

namespace WorkoutLibraryAndTracker.Pages.WorkoutLogs
{
    public class EditModel : PageModel
    {
        private readonly WorkoutLibraryAndTracker.Data.WorkoutDbContext _context;

        public EditModel(WorkoutLibraryAndTracker.Data.WorkoutDbContext context)
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

            var workoutlog =  await _context.WorkoutLogs.FirstOrDefaultAsync(m => m.WorkoutLogId == id);
            if (workoutlog == null)
            {
                return NotFound();
            }
            WorkoutLog = workoutlog;
           ViewData["WorkoutId"] = new SelectList(_context.Workouts, "WorkoutId", "WorkoutId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(WorkoutLog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkoutLogExists(WorkoutLog.WorkoutLogId))
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

        private bool WorkoutLogExists(int id)
        {
            return _context.WorkoutLogs.Any(e => e.WorkoutLogId == id);
        }
    }
}
