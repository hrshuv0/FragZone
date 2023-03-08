using Core.Dtos.Identity;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AuthController : BaseApiController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        try
        {
            var user = await _authService.Register(registerDto);

            return Ok(user);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }

        return BadRequest("Failed to register");
    }
}