using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WorkoutLibraryAndTracker.Models
    {
    public class WorkoutLog
        {
        public int WorkoutLogId { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }

        // Foreign key
        public int WorkoutId { get; set; }
        // Navigation Property
        [BindNever]
        public Workout? Workout { get; set; }
        }
    }
