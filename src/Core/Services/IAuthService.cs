using Core.Dtos.Identity;

namespace Core.Services;

public interface IAuthService
{
    Task<UserDetailsDto> Register(RegisterDto userDto);
    Task<(UserDetailsDto, string)> Login(LoginDto userDto);
    Task<bool> UserExists(string username);
}