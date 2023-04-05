namespace API.Dtos.Game;

public class GameDto
{
    public string? Name { get; set; }
    public string? Mode { get; set; }

    public long? CategoryId { get; set; }
    public string? Category { get; set; }
    
    public long? PublisherId { get; set; }
    public string? Publisher { get; set; }
    
    public DateTime CreatedTime { get; set; }
    public DateTime UpdatedTime { get; set; }
}