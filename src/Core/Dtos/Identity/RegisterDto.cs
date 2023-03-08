using System.ComponentModel.DataAnnotations;

namespace Core.Dtos.Identity;

public class RegisterDto
{
    [Required]
    public string? UserName { get; set; }
    
    [Required]
    public string? Email { get; set; }
    
    public string? InGameName { get; set; }
    
    [Required]
    public DateTime DateOfBirth { get; set; }

    [Required]
    public string? Password { get; set; }
}