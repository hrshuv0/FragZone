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
    [MinLength(4, ErrorMessage = "Password minimum lenght must be 4")]
    public string? Password { get; set; }
}