using WonderPlane.Shared;

namespace WonderPlane.Client.Servicios
{
    public interface IUserService
    {
        Task<string> CreateUser(RegisterDTO user);


    }
}
