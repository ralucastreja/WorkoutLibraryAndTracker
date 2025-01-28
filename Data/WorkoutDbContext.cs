using Microsoft.EntityFrameworkCore;
using WorkoutLibraryAndTracker.Models;

namespace WorkoutLibraryAndTracker.Data
    {
    public class WorkoutDbContext : DbContext
        {
        public WorkoutDbContext(DbContextOptions<WorkoutDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<WorkoutLog> WorkoutLogs { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<WorkoutEquipment> WorkoutEquipments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
            modelBuilder.Entity<WorkoutEquipment>()
                .HasKey(we => new { we.WorkoutId, we.EquipmentId });

            modelBuilder.Entity<Workout>()
                .HasOne(w => w.Category)
                .WithMany(c => c.Workouts)
                .HasForeignKey(w => w.CategoryId);

            modelBuilder.Entity<WorkoutLog>()
                .HasOne(wl => wl.Workout)
                .WithMany(w => w.WorkoutLogs)
                .HasForeignKey(wl => wl.WorkoutId);
            }
        }
    }
