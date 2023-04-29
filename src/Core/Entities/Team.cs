using Core.Entities.Identity;

namespace Core.Entities;

public class Team : BaseEntity<Guid>
{
    private const int MaxMemberCount = 20;
    
    public int MemberCount { get; set; } = 5;
    
    public string? Name { get; set; }
    public string? Location { get; set; } 
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }

    public ICollection<ApplicationUser>? MemberList { get; set; }
    
    public int SetMemberCount
    {
        get => MemberCount;
        set => MemberCount = value > MaxMemberCount ? MaxMemberCount : value;
    }
}