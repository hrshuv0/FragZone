﻿using Core.Common.Exceptions;
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
}