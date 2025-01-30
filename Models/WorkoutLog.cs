namespace WorkoutLibraryAndTracker.Models
    {
    public class WorkoutLog
        {
        public int WorkoutLogId { get; set; }
        public DateTime Date { get; set; }
        public int Duration { get; set; } // Duration completed in minutes
        public string Notes { get; set; }

        // Foreign key
        public int WorkoutId { get; set; }
        // Navigation Property
        public Workout Workout { get; set; }
        }
    }
