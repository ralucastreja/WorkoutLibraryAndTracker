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

        public SelectList CategorySelectList { get; set; }
        public SelectList EquipmentSelectList { get; set; }

        [BindProperty]
        public Workout Workout { get; set; } = default!;

        [BindProperty]
        public WorkoutLog WorkoutLog { get; set; } = default!;

        [BindProperty]
        public List<int> SelectedEquipmentIds { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
            {
            var categories = await _context.Categories.ToListAsync();
            CategorySelectList = new SelectList(categories, "CategoryId", "Name");

            var equipments = await _context.Equipments.ToListAsync();
            EquipmentSelectList = new SelectList(equipments, "EquipmentId", "Name");

            return Page();
            }

        public async Task<IActionResult> OnPostAsync()
            {
            if (!ModelState.IsValid)
                {
                var categories = await _context.Categories.ToListAsync();
                CategorySelectList = new SelectList(categories, "CategoryId", "Name");

                var equipments = await _context.Equipments.ToListAsync();
                EquipmentSelectList = new SelectList(equipments, "EquipmentId", "Name");

                return Page();
                }

            if (Workout.CategoryId == 0)
                {
                ModelState.AddModelError("Workout.CategoryId", "You must select a category.");

                var categories = await _context.Categories.ToListAsync();
                CategorySelectList = new SelectList(categories, "CategoryId", "Name");

                var equipments = await _context.Equipments.ToListAsync();
                EquipmentSelectList = new SelectList(equipments, "EquipmentId", "Name");

                return Page();
                }

            _context.Workouts.Add(Workout);
            await _context.SaveChangesAsync();

            if (WorkoutLog == null)
                {
                WorkoutLog = new WorkoutLog();
                }

            WorkoutLog.WorkoutId = Workout.WorkoutId;
            _context.WorkoutLogs.Add(WorkoutLog);
            await _context.SaveChangesAsync();

           
            if (SelectedEquipmentIds != null && SelectedEquipmentIds.Any())
                {
                foreach (var equipmentId in SelectedEquipmentIds)
                    {
                    _context.WorkoutEquipments.Add(new WorkoutEquipment
                        {
                        WorkoutId = Workout.WorkoutId,
                        EquipmentId = equipmentId
                        });
                    }
                await _context.SaveChangesAsync();
                }

            return RedirectToPage("./Index");
            }
        }
    }
