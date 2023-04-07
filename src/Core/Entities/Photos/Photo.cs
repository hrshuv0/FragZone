using Core.Entities.Identity;

namespace Core.Entities.Photos;

public class Photo : BaseEntity<Guid>
{
    public string? Url { get; set; }
    public bool IsMain { get; set; }
    public string? PublicId { get; set; }
    public string? Description { get; set; }
    public DateTime DateAdded { get; set; }
    public string? AppUserId { get; set; }
    public ApplicationUser? AppUser { get; set; }
    
    public Photo()
    {
        DateAdded = DateTime.Now;
    }
}