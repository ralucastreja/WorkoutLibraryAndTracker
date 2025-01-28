namespace WorkoutLibraryAndTracker.Models
    {
    public class Equipment
        {
        public int EquipmentId { get; set; }
        public string Name { get; set; }

        // Navigation Property
        public ICollection<WorkoutEquipment> WorkoutEquipments { get; set; }
        }
    }
