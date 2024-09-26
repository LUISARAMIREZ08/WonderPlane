using WonderPlane.Shared;

namespace WonderPlane.Client.Servicios
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetUsers();
        Task<UserDTO> GetUser(int id);
        Task<int> CreateUser(UserDTO user);
        Task<UserDTO> UpdateUser(UserDTO user);
        Task<UserDTO> DeleteUser(int id);

    }
}
