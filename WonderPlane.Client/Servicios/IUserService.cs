using WonderPlane.Shared;

namespace WonderPlane.Client.Servicios
{
    public interface IUserService
    {
        Task<string> CreateUser(UserRegisterDto user);
        Task<string> LoginUser(UserLoginDto user);
        Task<string> CreateAdmin(CreateAdminDto user);
        Task<UserInfo?> GetUserById(int id);
        Task<string> UpdateUserAsync(UserInfo user);
        Task<string> ChangePasswordAsync(ChangePasswordDto user);

    }
}
