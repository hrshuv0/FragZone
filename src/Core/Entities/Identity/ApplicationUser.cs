using Core.Entities.Photos;
using Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Identity;

public class ApplicationUser : IdentityUser
{
    
    public string? DisplayName { get; set; }
    public string? InGameName { get; set; }
    public DateTime DateOfBirth { get; set; }


    public DateTime CreatedTime { get; set; }
    public DateTime LastActive { get; set; }
    public StatusUser Status { get; set; }

    public Guid? TeamId { get; set; }
    public Team? Team { get; set; }
    public bool IsLeader { get; set; }

    public IList<Photo>? Photos { get; set; }

    public ApplicationUser()
    {
        CreatedTime = DateTime.Now;
        LastActive = DateTime.Now;
        Status = StatusUser.Active;
        IsLeader = false;
    }
}