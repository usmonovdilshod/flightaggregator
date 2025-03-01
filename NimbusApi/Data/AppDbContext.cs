using Microsoft.EntityFrameworkCore;
using NimbusApi.Entity;

namespace NimbusApi.Data;

public partial class AppDbContext : DbContext
{
    public IServiceScopeFactory _serviceScopeFactory;

    [ActivatorUtilitiesConstructor]
    public AppDbContext(DbContextOptions<AppDbContext> options, 
      IServiceScopeFactory serviceScopeFactory) : base(options)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public DbSet<FlightEntity> Flights { get; set; } = null!;
    public DbSet<BookEntity> Books { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FlightEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("flight_pkey");

            entity.ToTable("flights");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Airline).HasColumnName("airline");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.DepartureAirportCode).HasColumnName("departure_airport_code");
            entity.Property(e => e.DestinationAirportCode).HasColumnName("destination_airport_code");
            entity.Property(e => e.DepartureDate).HasColumnName("departure_date");
            entity.Property(e => e.ArrivalDate).HasColumnName("arrival_date");
            entity.Property(e => e.Layovers).HasColumnName("layovers").HasDefaultValue(0);

            entity.HasIndex(e => e.DepartureAirportCode);
            entity.HasIndex(e => e.DestinationAirportCode);
            entity.HasIndex(e => e.Airline);
        });

        modelBuilder.Entity<BookEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("book_pkey");

            entity.ToTable("books");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.UserId)
                .HasColumnName("user_id");
            entity.Property(e => e.FlightId).HasColumnName("flight_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnName("created_at");

            entity.HasOne(e => e.Flight)
                  .WithMany()  
                  .HasForeignKey(e => e.FlightId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => e.UserId)
                  .HasDatabaseName("ix_books_user_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


}
