namespace WorkoutLibraryAndTracker.Models
    {
    public class Workout
        {
        public int WorkoutId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; } // Duration in minutes
        public string Intensity { get; set; }

        // Foreign Key
        public int CategoryId { get; set; }

        // Navigation Properties
        public Category Category { get; set; }
        public ICollection<WorkoutLog> WorkoutLogs { get; set; }
        public ICollection<WorkoutEquipment> WorkoutEquipments { get; set; }
        }
    }
