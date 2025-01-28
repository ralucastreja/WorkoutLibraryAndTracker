namespace WorkoutLibraryAndTracker.Models
    {
    public class WorkoutEquipment
        {
        public int WorkoutId { get; set; }
        public int EquipmentId { get; set; }

        // Navigation Properties
        public Workout Workout { get; set; }
        public Equipment Equipment { get; set; }
        }
    }
