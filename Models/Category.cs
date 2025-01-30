namespace WorkoutLibraryAndTracker.Models
    {
    public class Category
        {
        public int CategoryId { get; set; }
        public required string Name { get; set; }

        // Navigation Property
        public required ICollection<Workout> Workouts { get; set; } = [];
        }
    }
