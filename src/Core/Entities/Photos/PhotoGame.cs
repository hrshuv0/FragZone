namespace Core.Entities.Photos;

public class PhotoGame : BaseEntity<long>
{
    public string? Url { get; set; }
    public bool IsMain { get; set; }
    public string? PublicId { get; set; }

    public long GameId { get; set; }
    public Game? Game { get; set; }
    
}