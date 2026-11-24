using Microsoft.EntityFrameworkCore;
using EventRegistrationAPI_IlliaTkach.Models;


public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Event> Events { get; set; }
    public DbSet<Registration> Registrations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Event>().HasData(
            new Event
            {
                EventId = 1,
                Title = "Orientation",
                Location = "Room E202",
                EventDate = new DateTime(2026, 9, 12, 10, 0, 0),
                Capacity = 100
            },
            new Event
            {
                EventId = 2,
                Title = "AI Workshop",
                Location = "AI Lab",
                EventDate = new DateTime(2027, 6, 15, 14, 0, 0),
                Capacity = 30
            },
            new Event
            {
                EventId = 3,
                Title = "Career Mentorship Session",
                Location = "Room B200",
                EventDate = new DateTime(2027, 6, 20, 11, 0, 0),
                Capacity = 2
            },
            new Event
            {
                EventId = 4,
                Title = "AI Seminar",
                Location = "Lecture Hall",
                EventDate = new DateTime(2027, 7, 10, 13, 0, 0),
                Capacity = 50
            }
        );

        modelBuilder.Entity<Registration>().HasData(
            new Registration
            {
                RegistrationId = 1,
                EventId = 3,
                StudentName = "John Doe",
                Email = "john@gmail.com",
                RegisteredOn = new DateTime(2026, 11, 20)
            },
            new Registration
            {
                RegistrationId = 2,
                EventId = 3,
                StudentName = "Illia Tkach",
                Email = "illia@gmail.com",
                RegisteredOn = new DateTime(2026, 11, 21)
            }
        );
    }
}