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

        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


}
