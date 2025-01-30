using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutLibraryAndTracker.Migrations
{
    /// <inheritdoc />
    public partial class RemoveWorkoutLogDuration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "WorkoutLogs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "WorkoutLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
