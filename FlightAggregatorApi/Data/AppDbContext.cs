using FlightAggregatorApi.Entity;
using Microsoft.EntityFrameworkCore;

namespace FlightAggregatorApi.Data;

public partial class AppDbContext : DbContext
{
    public IServiceScopeFactory _serviceScopeFactory;

    [ActivatorUtilitiesConstructor]
    public AppDbContext(DbContextOptions<AppDbContext> options,
      IServiceScopeFactory serviceScopeFactory) : base(options)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public DbSet<Credential> Credentials { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Credential>(entity =>
        {
            entity.HasKey(e => e.UserId)
                  .HasName("credential_pkey");

            entity.ToTable("credentials");

            entity.Property(e => e.UserId)
                  .ValueGeneratedNever()  
                  .HasColumnName("user_id");

            entity.Property(e => e.AccessToken)
                  .HasColumnName("access_token");

            entity.Property(e => e.RefreshToken)
                  .HasColumnName("refresh_token");

            entity.Property(e => e.ExpiresInSeconds)
                  .HasColumnName("expires_in_seconds");

            entity.Property(e => e.IdToken)
                  .HasColumnName("id_token");

            entity.Property(e => e.IssuedUtc)
                  .HasColumnName("issued_utc");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);




}
