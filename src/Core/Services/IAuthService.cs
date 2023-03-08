using Core.Dtos.Identity;
using Core.Entities.Identity;

namespace Core.Services;

public interface IAuthService
{
    Task<UserDetailsDto> Register(RegisterDto userDto);
    Task<UserDetailsDto> Login(LoginDto userDto);
    Task<bool> UserExists(string username);
}