using Core.Common.Exceptions;
using Core.Dtos.Identity;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AuthController : BaseApiController
{
    private readonly IAuthService _authService;

    public AuthController(ILoggerFactory factory, IAuthService authService)
    {
        _logger = factory.CreateLogger<AuthController>();
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        // throw new Exception("Test exception");
        try
        {
            var user = await _authService.Register(registerDto);

            return Ok(user);
        }
        catch (FragException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }

        return BadRequest("Register failed");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        try
        {
            var userExists = await _authService.UserExists(loginDto.UserName!);
            if (!userExists)
                return BadRequest("There is not account with this username");

            var token = await _authService.Login(loginDto);

            if (token is null)
                return BadRequest("Username or password did not matched");

            return Ok(new
            {
                Token = token
            });
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message);
            return Unauthorized("Login failed");
        }
    }
}