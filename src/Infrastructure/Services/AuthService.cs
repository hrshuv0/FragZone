using Core.Common.Exceptions;
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
                throw new FragException("Username already exists");
            
            if(await _authRepository.UserEmailExists(userDto.Email))
                throw new FragException("Email already in used");

            var userToCreate = new ApplicationUser()
            {
                UserName = userDto.UserName,
                Email = userDto.Email,
                DisplayName = userDto.UserName
            };

            var user = await _authRepository.Register(userToCreate, userDto.Password!);

            if (user is null)
                throw new FragException("Unable to register");

            var userToReturn = new UserDetailsDto()
            {
                UserName = user.UserName,
                DisplayName = user.DisplayName,
                Email = user.Email
            };

            return userToReturn;
        }
        catch (FragException ex)
        {
            throw new FragException(ex.Message);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<(UserDetailsDto, string)> Login(LoginDto userDto)
    {
        try
        {
            var user = await _authRepository.Login(userDto.UserName!, userDto.Password!);

            if (user is null)
                throw new FragException("Username or password did not matched");

            var userToReturn = new UserDetailsDto()
            {
                UserName = user.UserName,
                Email = user.Email,
                DisplayName = user.DisplayName
            };

            var token = _authRepository.CreateToken(user);

            return (userToReturn, token);
        }
        catch (FragException ex)
        {
            throw new FragException(ex.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> UserExists(string username)
    {
        try
        {
            return await _authRepository.UserExists(username);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}