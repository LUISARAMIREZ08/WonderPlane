using WonderPlane.Shared;

namespace WonderPlane.Client.Servicios
{
    public interface IFlightService
    {
        Task <string> CreateFlight(FlightDTO flight);
    }
}
