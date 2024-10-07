using WonderPlane.Shared;

namespace WonderPlane.Client.Servicios
{
    public interface IUserService
    {
        Task<string> CreateUser(UserRegisterDto user);
        Task<string> LoginUser(UserLoginDto user);

    }
}
