namespace WorkoutLibraryAndTracker.Models
    {
    public class Category
        {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        // Navigation Property
        public ICollection<Workout> Workouts { get; set; }
        }
    }
