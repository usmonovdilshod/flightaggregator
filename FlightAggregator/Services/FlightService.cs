using FlightAggregator.Models;
using Microsoft.Extensions.Caching.Memory;

namespace FlightAggregator.Services
{
    public class FlightService
    {
        private readonly IMemoryCache _cache;
        private readonly ILogger<FlightService> _logger;

        public FlightService(IMemoryCache cache, ILogger<FlightService> logger)
        {
            _cache = cache;
            _logger = logger;
        }

        // Simulated external API calls
        private List<Flight> GetFlightsFromTestAPI1()
        {
            return new List<Flight>
            {
                new Flight { FlightNumber = "AA123", Airline = "AirlineA", Departure = "NYC", Arrival = "LAX", DepartureTime = DateTime.Now.AddHours(5), ArrivalTime = DateTime.Now.AddHours(10), Price = 350, Stops = 1, Source = "TestAPI1" },
                new Flight { FlightNumber = "BB456", Airline = "AirlineB", Departure = "LAX", Arrival = "ORD", DepartureTime = DateTime.Now.AddHours(3), ArrivalTime = DateTime.Now.AddHours(6), Price = 280, Stops = 0, Source = "TestAPI1" }
            };
        }

        private List<Flight> GetFlightsFromTestAPI2()
        {
            return new List<Flight>
            {
                new Flight { FlightNumber = "CC789", Airline = "AirlineC", Departure = "NYC", Arrival = "MIA", DepartureTime = DateTime.Now.AddHours(4), ArrivalTime = DateTime.Now.AddHours(8), Price = 320, Stops = 1, Source = "TestAPI2" },
                new Flight { FlightNumber = "DD101", Airline = "AirlineD", Departure = "MIA", Arrival = "LAX", DepartureTime = DateTime.Now.AddHours(6), ArrivalTime = DateTime.Now.AddHours(12), Price = 400, Stops = 2, Source = "TestAPI2" }
            };
        }

        // Aggregate flight data with caching
        public async Task<List<Flight>> SearchFlights(string departure, string arrival)
        {
            string cacheKey = $"flights_{departure}_{arrival}";

            if (!_cache.TryGetValue(cacheKey, out List<Flight> flights))
            {
                _logger.LogInformation($"Fetching flights for {departure} -> {arrival}");

                // Simulate async API requests
                await Task.Delay(500);
                flights = GetFlightsFromTestAPI1().Concat(GetFlightsFromTestAPI2())
                    .Where(f => f.Departure == departure && f.Arrival == arrival)
                    .ToList();

                // Cache results for 5 minutes
                _cache.Set(cacheKey, flights, TimeSpan.FromMinutes(5));
            }
            else
            {
                _logger.LogInformation($"Cache hit for {departure} -> {arrival}");
            }

            return flights;
        }

        // Mock Booking API
        public bool BookFlight(string flightNumber)
        {
            _logger.LogInformation($"Booking flight {flightNumber}");
            return true; // Assume booking is always successful
        }
    }
}
