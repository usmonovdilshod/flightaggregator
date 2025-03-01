using FlightAggregatorApi.Abstracts;
using FlightAggregatorShared.Models;
using System.Text.Json;

namespace FlightAggregatorApi.Services;

public class SearchService : ISearchService, IDisposable
{
    #region Initialize
    private readonly IHttpClientFactory _httpClient;
    private readonly IConfiguration _configuration;
    private readonly HttpClient _client;
    private readonly ILogger<SearchService> _logger;

    public SearchService(IHttpClientFactory httpClientFactory, IConfiguration configuration,
         ILogger<SearchService> logger)
    {
        _httpClient = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _logger = logger;

        _client = _httpClient.CreateClient("External");
    }
    #endregion

    public async Task<ApiResponse<FlightResponse>> SearchFlights(ApiOptions options, CancellationToken cancellationToken)
    {
        var nimbusUrl = _configuration["ExternalUrls:Nimbus"];
        var skylinkUrl = _configuration["ExternalUrls:SkyLink"];

        var queryParams = new List<string>();

        if (!string.IsNullOrWhiteSpace(options.DepartureAirportCode))
            queryParams.Add($"departureAirportCode={Uri.EscapeDataString(options.DepartureAirportCode)}");

        if (!string.IsNullOrWhiteSpace(options.DestinationAirportCode))
            queryParams.Add($"destinationAirportCode={Uri.EscapeDataString(options.DestinationAirportCode)}");

        if (!string.IsNullOrWhiteSpace(options.Airline))
            queryParams.Add($"airline={Uri.EscapeDataString(options.Airline)}");

        if (options.MinPrice.HasValue)
            queryParams.Add($"minPrice={options.MinPrice.Value}");

        if (options.MaxPrice.HasValue)
            queryParams.Add($"maxPrice={options.MaxPrice.Value}");

        if (options.DepartureDate.HasValue)
            queryParams.Add($"departureDate={options.DepartureDate.Value:yyyy-MM-dd}");

        if (options.MaxLayovers.HasValue)
            queryParams.Add($"maxLayovers={options.MaxLayovers.Value}");

        var queryString = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : string.Empty;

        _logger.LogInformation("Starting flight search with options: {@Options}. Query: {QueryString}", options, queryString);

        try
        {
            var nimbusTask = _client.GetAsync($"{nimbusUrl}/api/flights/search{queryString}", cancellationToken);
            var skylinkTask = _client.GetAsync($"{skylinkUrl}/api/flights/search{queryString}", cancellationToken);

            await Task.WhenAll(nimbusTask, skylinkTask);

            var nimbusResponse = await nimbusTask;
            var skylinkResponse = await skylinkTask;

            List<FlightView> nimbusFlights = [];
            List<FlightView> skylinkFlights = [];

            if (nimbusResponse.IsSuccessStatusCode)
            {
                var nimbusContent = await nimbusResponse.Content.ReadAsStringAsync(cancellationToken);
                nimbusFlights = JsonSerializer.Deserialize<List<FlightView>>(nimbusContent,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? [];
            }
            else
            {
                _logger.LogWarning("Nimbus API returned status code {StatusCode}", nimbusResponse.StatusCode);
            }

            if (skylinkResponse.IsSuccessStatusCode)
            {
                var skylinkContent = await skylinkResponse.Content.ReadAsStringAsync(cancellationToken);
                skylinkFlights = JsonSerializer.Deserialize<List<FlightView>>(skylinkContent,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? [];
            }
            else
            {
                _logger.LogWarning("SkyLink API returned status code {StatusCode}", skylinkResponse.StatusCode);
            }

            var allFlights = nimbusFlights.MapToViewList("Nimbus")
                .Concat(skylinkFlights.MapToViewList("SkyLink"))
                .ToList();

            allFlights = allFlights.OrderBy(f => f.Price)
                                   .ThenBy(f => f.DepartureAirportCode)
                                   .ToList();
            var sortedBookings = Sorting(allFlights, options).ToList();
            var count = sortedBookings.Count;
            _logger.LogInformation("Flight search completed. Found {Count} flights.", count);
            return new ApiResponse<FlightResponse>() { Items = sortedBookings , TotalItems = count };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred during flight search.");
            throw;
        }
    }

    #region Helpers

    private IEnumerable<FlightResponse> Sorting(IEnumerable<FlightResponse> bookings, ApiOptions options)
    {
        return options.SortLabel switch
        {
            "Price" => options.SortDirection == 1
                            ? bookings.OrderBy(b => b.Price)
                            : bookings.OrderByDescending(b => b.Price),
            "Layovers" => options.SortDirection == 1
                            ? bookings.OrderBy(b => b.Layovers)
                            : bookings.OrderByDescending(b => b.Layovers),
            "Airline" => options.SortDirection == 1
                            ? bookings.OrderBy(b => b.Airline)
                            : bookings.OrderByDescending(b => b.Airline),
            _ => bookings.OrderBy(b => b.Id),
        };
    }
    public void Dispose()
    {
        _client.Dispose();
    }
    #endregion
}
