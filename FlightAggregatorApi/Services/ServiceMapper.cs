using FlightAggregatorShared.Models;
using Riok.Mapperly.Abstractions;

namespace FlightAggregatorApi.Services;

[Mapper]
public static partial class ServiceMapper
{
    public static FlightResponse MapToView(this FlightView src, string source) => src.To(source);
    public static List<FlightResponse> MapToViewList(this List<FlightView> src, string source) 
        => src.ToList(source);
    public static BookResponse MapToView(this BookView src, string source) => src.ToBook(source);
    public static List<BookResponse> MapToViewList(this List<BookView> src, string source) 
        => src.ToBookList(source);


    #region Internal
    //private static partial BookResponse ToBook(this BookView src);
    //private static partial List<BookResponse> ToBookList(this List<BookView> src);

    #endregion


    [global::System.CodeDom.Compiler.GeneratedCode("Riok.Mapperly", "4.1.1.0")]
    private static global::System.Collections.Generic.List<global::FlightAggregatorShared.Models.FlightResponse> ToList(this global::System.Collections.Generic.List<global::FlightAggregatorShared.Models.FlightView> src, string source)
    {
        var target = new global::System.Collections.Generic.List<global::FlightAggregatorShared.Models.FlightResponse>(src.Count);
        foreach (var item in src)
        {
            target.Add(MapToView(item, source));
        }
        return target;
    }

    [global::System.CodeDom.Compiler.GeneratedCode("Riok.Mapperly", "4.1.1.0")]
    private static global::FlightAggregatorShared.Models.FlightResponse To(this global::FlightAggregatorShared.Models.FlightView src, string source)
    {
        var target = new global::FlightAggregatorShared.Models.FlightResponse();
        target.Id = src.Id;
        target.Airline = src.Airline;
        target.Price = src.Price;
        target.DepartureAirportCode = src.DepartureAirportCode;
        target.DestinationAirportCode = src.DestinationAirportCode;
        target.DepartureDate = src.DepartureDate;
        target.ArrivalDate = src.ArrivalDate;
        target.Layovers = src.Layovers;
        target.Source = source;
        return target;
    }




    [global::System.CodeDom.Compiler.GeneratedCode("Riok.Mapperly", "4.1.1.0")]
    private static global::System.Collections.Generic.List<global::FlightAggregatorShared.Models.BookResponse> ToBookList(this global::System.Collections.Generic.List<global::FlightAggregatorShared.Models.BookView> src, string source)
    {
        var target = new global::System.Collections.Generic.List<global::FlightAggregatorShared.Models.BookResponse>(src.Count);
        foreach (var item in src)
        {
            target.Add(MapToView(item, source));
        }
        return target;
    }


    [global::System.CodeDom.Compiler.GeneratedCode("Riok.Mapperly", "4.1.1.0")]
    private static global::FlightAggregatorShared.Models.BookResponse ToBook(this global::FlightAggregatorShared.Models.BookView src, string source)
    {
        var target = new global::FlightAggregatorShared.Models.BookResponse();
        target.UserId = src.UserId;
        target.FlightId = src.FlightId;
        target.Flight = src.Flight;
        target.CreatedAt = src.CreatedAt;
        target.Source = source;
        return target;
    }
}
