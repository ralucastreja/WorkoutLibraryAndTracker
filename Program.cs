using Microsoft.EntityFrameworkCore;
using WorkoutLibraryAndTracker.Data;

var builder = WebApplication.CreateBuilder(args);

// Retrieve the correct connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("WorkoutDb");

if (string.IsNullOrEmpty(connectionString))
    {
    throw new InvalidOperationException("Connection string 'WorkoutDb' not found.");
    }

builder.Services.AddDbContext<WorkoutDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddRazorPages();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
app.Run();
