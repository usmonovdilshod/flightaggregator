namespace FlightAggregator.Models
{
    public class Flight
    {
        public string FlightNumber { get; set; }
        public string Airline { get; set; }
        public string Departure { get; set; }
        public string Arrival { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public decimal Price { get; set; }
        public int Stops { get; set; }
        public string Source { get; set; }  
    }
}
