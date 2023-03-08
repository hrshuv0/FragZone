using Core.Dtos.Identity;
using Core.Entities.Identity;
using Core.Repositories;
using Core.Services;

namespace Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;

    public AuthService(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<UserDetailsDto> Register(RegisterDto userDto)
    {
        try
        {
            userDto.UserName = userDto.UserName!.Normalize();
            userDto.Email = userDto.Email!.Normalize();
            
            if(await _authRepository.UserNameExists(userDto.UserName))
                throw new Exception("Username already exists");
            
            if(await _authRepository.UserEmailExists(userDto.Email))
                throw new Exception("Email already in used");

            var userToCreate = new ApplicationUser()
            {
                UserName = userDto.UserName,
                Email = userDto.Email
            };

            var user = await _authRepository.Register(userToCreate, userDto.Password!);

            var userToReturn = new UserDetailsDto()
            {
                UserName = user.UserName,
                DisplayName = user.DisplayName,
                Email = user.Email
            };

            return userToReturn;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task<UserDetailsDto> Login(LoginDto userDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UserExists(string username)
    {
        throw new NotImplementedException();
    }
}