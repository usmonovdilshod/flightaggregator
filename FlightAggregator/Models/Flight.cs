namespace FlightAggregator.Models
{
    public class FlightRequest
    {
        public string Origin { get; set; } = null!;
        public string Destination { get; set; } = null!;
        public DateTime DepartureDate { get; set; } 
        public DateTime ReturnDate { get; set; }
        public int PassengerCount { get; set; }
    }

    public class FlightResponse
    {
        public string AirlineName { get; set; } = null!;
        public string Supplier { get; set; } = null!;
        public string Fare { get; set; } = null!;
        public string DepartureAirportCode { get; set; } = null!;
        public string DestinationAirportCode { get; set; } = null!;
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; } 
    }
}
