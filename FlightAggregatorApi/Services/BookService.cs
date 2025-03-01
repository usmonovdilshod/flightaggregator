using FlightAggregatorApi.Abstracts;
using FlightAggregatorShared.Models;
using System.Text;
using System.Text.Json;

namespace FlightAggregatorApi.Services;

public class BookService : IBookService, IDisposable
{
    #region Initialize
    private readonly IHttpClientFactory _httpClient;
    private readonly IConfiguration _configuration;
    private readonly HttpClient _client;
    private readonly ILogger<BookService> _logger;

    public BookService(IHttpClientFactory httpClientFactory, IConfiguration configuration,
         ILogger<BookService> logger)
    {
        _httpClient = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _logger = logger;

        _client = _httpClient.CreateClient("External");
    }
    #endregion

    public async Task<long> Create(string userId, long flightId, string source, 
        CancellationToken cancellationToken)
    {
      
        var serviceUrl = _configuration[$"ExternalUrls:{source}"];
        if (string.IsNullOrWhiteSpace(serviceUrl))
        {
            _logger.LogError("No external URL configured for source {Source}", source);
            throw new InvalidOperationException($"No external URL configured for source {source}");
        }

        var bookingRequest = new BookCreateExternalRequest
        {
            UserId = userId,
            FlightId = flightId
        };

        var json = JsonSerializer.Serialize(bookingRequest);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        _logger.LogInformation("Sending booking request for User: {UserId}, Flight: {FlightId}, Source: {Source}", userId, flightId, source);

        var response = await _client.PostAsync($"{serviceUrl}/api/books", content, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

            long bookingId;
            try
            {
                bookingId = JsonSerializer.Deserialize<long>(responseContent,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Booking response deserialization failed for source {Source}. Response: {ResponseContent}", source, responseContent);
                throw new Exception("Booking response deserialization failed.");
            }

            _logger.LogInformation("Booking successful. BookingId: {BookingId}", bookingId);
            return bookingId;
        }
        else
        {
            _logger.LogError("Booking request failed for source {Source} with status code {StatusCode}", source, response.StatusCode);
            throw new Exception($"Booking request failed with status code {response.StatusCode}");
        }

    }


    public async Task<ApiResponse<BookResponse>> GetAll(string userId, ApiOptions options,
        CancellationToken cancellationToken)
    {
        var nimbusUrl = _configuration["ExternalUrls:Nimbus"];
        var skylinkUrl = _configuration["ExternalUrls:SkyLink"];

        var nimbusUri = $"{nimbusUrl}/api/books?userId={Uri.EscapeDataString(userId)}";
        var skylinkUri = $"{skylinkUrl}/api/books?userId={Uri.EscapeDataString(userId)}";

        _logger.LogInformation("Fetching all bookings for User: {UserId}", userId);

        var nimbusTask = _client.GetAsync(nimbusUri, cancellationToken);
        var skylinkTask = _client.GetAsync(skylinkUri, cancellationToken);

        await Task.WhenAll(nimbusTask, skylinkTask);

        var nimbusResponse = await nimbusTask;
        var skylinkResponse = await skylinkTask;

        List<BookView> nimbusBookings = [];
        List<BookView> skylinkBookings = [];

        if (nimbusResponse.IsSuccessStatusCode)
        {
            var content = await nimbusResponse.Content.ReadAsStringAsync(cancellationToken);
            nimbusBookings = JsonSerializer.Deserialize<List<BookView>>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? [];
        }
        else
        {
            _logger.LogWarning("Nimbus booking API returned status code {StatusCode}", nimbusResponse.StatusCode);
        }

        if (skylinkResponse.IsSuccessStatusCode)
        {
            var content = await skylinkResponse.Content.ReadAsStringAsync(cancellationToken);
            skylinkBookings = JsonSerializer.Deserialize<List<BookView>>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? [];
        }
        else
        {
            _logger.LogWarning("SkyLink booking API returned status code {StatusCode}", skylinkResponse.StatusCode);
        }

        var allBookings = nimbusBookings.MapToViewList("Nimbus")
            .Concat(skylinkBookings.MapToViewList("SkyLink"))
            .ToList();

        var sortedBookings = Sorting(allBookings, options).ToList();
        var count = sortedBookings.Count;
        _logger.LogInformation("Fetched total {Count} bookings for User: {UserId}", count, userId);

        return new ApiResponse<BookResponse>() { Items = sortedBookings, TotalItems = count };
    }

    #region Helpers

    private IEnumerable<BookResponse> Sorting(IEnumerable<BookResponse> bookings, ApiOptions options)
    {
        return options.SortLabel switch
        {
            "CreatedAt" => options.SortDirection == 1
                            ? bookings.OrderBy(b => b.CreatedAt)
                            : bookings.OrderByDescending(b => b.CreatedAt),
            
            _ => bookings.OrderBy(b => b.CreatedAt),
        };
    }

    public void Dispose()
    {
        _client.Dispose();
    }
    #endregion
}
