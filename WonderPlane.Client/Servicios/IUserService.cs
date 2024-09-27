using WonderPlane.Shared;

namespace WonderPlane.Client.Servicios
{
    public interface IUserService
    {
        Task<RegisterDTO> CreateUser(RegisterDTO user);


    }
}
