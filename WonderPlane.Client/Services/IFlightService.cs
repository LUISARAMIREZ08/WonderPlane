using WonderPlane.Shared;

namespace WonderPlane.Client.Services
{
    public interface IFlightService
    {
        Task <string> CreateFlight(FlightDTO flight);
        Task<FlightDTO?> SearchFlight(int id);
        Task<List<FlightDTO>> ListFlight();
        Task<ResponseAPI<FlightDTO>> UpdateFlight(int id, FlightDTO flightDTO);
        Task<ResponseAPI<FlightDTO>> GetFlightByCode(string code);

    }
}
