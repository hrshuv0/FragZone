﻿namespace Core.Dtos.Identity;

public class UserUpdateDto
{
    public string? Id { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? DisplayName { get; set; }
    public string? InGameName { get; set; }

    public DateTime CreatedTime { get; set; }
    public DateTime LastActive { get; set; }
    public string? Status { get; set; }
}