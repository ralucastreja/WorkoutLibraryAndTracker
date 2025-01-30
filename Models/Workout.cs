using System.ComponentModel.DataAnnotations;

namespace WorkoutLibraryAndTracker.Models
    {
    public class Workout
        {
        public int WorkoutId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public int Duration { get; set; } // Duration in minutes
        public required string Intensity { get; set; }

        // Foreign Key
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        // Navigation Properties
        public Category? Category { get; set; }
        public  ICollection<WorkoutLog> WorkoutLogs { get; set; } = [];
        public ICollection<WorkoutEquipment> WorkoutEquipments { get; set; } = [];
        }
    }
