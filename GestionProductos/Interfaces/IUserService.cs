using GestionProductos.DTOs.User;

namespace GestionProductos.Interfaces
{
    public interface IUserService
    {
        Task<UserTokenDto?> RegisterAsync(RegisterUserDto dto);
        Task<UserTokenDto?> LoginAsync(LoginUserDto dto);
        Task<IEnumerable<UserDto>> GetAllAsyn();
        Task UpdateAsync(int id, RegisterUserDto dto);
        Task<UserTokenDto?> RefreshToken(RefreshTokenDto dto);
    }
}
