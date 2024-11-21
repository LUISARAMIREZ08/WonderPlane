using System;


namespace WonderPlane.Shared;

public class FlightSearchRequest
{
    public required string Origin { get; set; }
    public required string Destination { get; set; }
    public required DateTime DepartureDate { get; set; }
    public required int Passengers { get; set; }
}


