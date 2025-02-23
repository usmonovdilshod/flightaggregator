using Microsoft.EntityFrameworkCore;
using SkyLinkApi.Abstracts;
using SkyLinkApi.Data;
using SkyLinkApi.Entity;
using SkyLinkApi.Models;

namespace SkyLinkApi.Services;

public class SearchService(AppDbContext context) : ISearchService
{
    public async Task<List<FlightEntity>> SearchFlights(ApiOptions options, CancellationToken cancellationToken)
    {
        var query = context.Flights.AsQueryable();

        if (!string.IsNullOrWhiteSpace(options.DepartureAirportCode) &&
            !string.IsNullOrWhiteSpace(options.DestinationAirportCode))
        {
            string departureCode = options.DepartureAirportCode.Trim().ToLower();
            string destinationCode = options.DestinationAirportCode.Trim().ToLower();

            query = query.Where(f => EF.Functions.Like(f.DepartureAirportCode.ToLower(), departureCode) &&
                                     EF.Functions.Like(f.DestinationAirportCode.ToLower(), destinationCode));
        }
        else if (!string.IsNullOrWhiteSpace(options.DepartureAirportCode))
        {
            string departureCode = options.DepartureAirportCode.Trim().ToLower();

            query = query.Where(f => EF.Functions.Like(f.DepartureAirportCode.ToLower(), departureCode));
        }
        else if (!string.IsNullOrWhiteSpace(options.DestinationAirportCode))
        {
            string destinationCode = options.DestinationAirportCode.Trim().ToLower();

            query = query.Where(f => EF.Functions.Like(f.DestinationAirportCode.ToLower(), destinationCode));
        }

        if (!string.IsNullOrEmpty(options.Airline))
        {
            query = query.Where(f => EF.Functions.Like(f.Airline.ToLower(), options.Airline.ToLower()));
        }

        if (options.MinPrice.HasValue)
        {
            query = query.Where(f => f.Price >= options.MinPrice.Value);
        }

        if (options.MaxPrice.HasValue)
        {
            query = query.Where(f => f.Price <= options.MaxPrice.Value);
        }

        if (options.DepartureDate.HasValue)
        {
            var departureDateStart = DateTime.SpecifyKind(options.DepartureDate.Value.Date, DateTimeKind.Utc);
            var departureDateEnd = departureDateStart.AddDays(1);

            query = query.Where(f => f.DepartureDate >= departureDateStart && f.DepartureDate < departureDateEnd);
        }

        if (options.MaxLayovers.HasValue)
        {
            query = query.Where(f => f.Layovers <= options.MaxLayovers.Value);
        }

        return await query.ToListAsync(cancellationToken);
    }
}
