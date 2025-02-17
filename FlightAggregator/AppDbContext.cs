using Microsoft.EntityFrameworkCore;

namespace FlightAggregator.Services;

public partial class AppDbContext : DbContext
{
    public IServiceScopeFactory _serviceScopeFactory;

    [ActivatorUtilitiesConstructor]
    public AppDbContext(DbContextOptions<AppDbContext> options, 
      IServiceScopeFactory serviceScopeFactory) : base(options)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    

   
}
