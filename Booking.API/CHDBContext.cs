using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Booking.API;

public class CHDBContext : DbContext
{
    public CHDBContext(DbContextOptions<CHDBContext> options) : base(options)
    {
    }
    public DbSet<Appointment> appointments { get; set; }

    public DbSet<Consultant> consultants { get; set; }

    public DbSet<Patient> patients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        new DbInitializer(modelBuilder).Seed();
    }
}

public class DbInitializer
{
    private readonly ModelBuilder _modelBuilder;

    public DbInitializer(ModelBuilder modelBuilder)
    {
        _modelBuilder = modelBuilder;
    }

    public void Seed()
    {

        _modelBuilder.Entity<Consultant>().HasData(
            new() { Id = 1, FName = "Jane", LName = "Doe", Speciality = "Dentist" },
            new() { Id = 2, FName = "Russel", LName = "Sprout", Speciality = "Pediatrist" },
            new() { Id = 3, FName = "Lance", LName = "Bogrol", Speciality = "Generalist" }
        );
        _modelBuilder.Entity<Appointment>().HasData(
            new() { Id = 1, ConsultantId = 2, StartDateTime = new(2023, 1, 1, 9, 0, 0), EndDateTime = new(2023, 1, 1, 10, 0, 0) },
            new() { Id = 2, ConsultantId = 2, StartDateTime = new(2023, 1, 1, 10, 0, 0), EndDateTime = new(2023, 1, 1, 11, 0, 0) },
            new() { Id = 3, ConsultantId = 2, StartDateTime = new(2023, 1, 2, 9, 0, 0), EndDateTime = new(2023, 1, 2, 10, 0, 0) },
            new() { Id = 4, ConsultantId = 1, StartDateTime = new(2023, 1, 1, 9, 0, 0), EndDateTime = new(2023, 1, 1, 10, 0, 0) },
            new() { Id = 5, ConsultantId = 1, StartDateTime = new(2023, 1, 2, 9, 0, 0), EndDateTime = new(2023, 1, 2, 10, 0, 0) },
            new() { Id = 6, ConsultantId = 1, StartDateTime = new(2023, 1, 3, 9, 0, 0), EndDateTime = new(2023, 1, 3, 10, 0, 0) },
            new() { Id = 7, ConsultantId = 1, StartDateTime = new(2023, 1, 3, 10, 0, 0), EndDateTime = new(2023, 1, 3, 11, 0, 0) },
            new() { Id = 8, ConsultantId = 1, StartDateTime = new(2023, 1, 3, 11, 0, 0), EndDateTime = new(2023, 1, 3, 12, 0, 0) },
            new() { Id = 9, ConsultantId = 1, StartDateTime = new(2023, 1, 3, 12, 0, 0), EndDateTime = new(2023, 1, 3, 13, 0, 0) },
            new() { Id = 10, ConsultantId = 1, StartDateTime = new(2023, 1, 3, 14, 0, 0), EndDateTime = new(2023, 1, 3, 15, 0, 0) },
            new() { Id = 11, ConsultantId = 1, StartDateTime = new(2023, 1, 3, 15, 0, 0), EndDateTime = new(2023, 1, 3, 16, 0, 0) }
        );
    }
}
