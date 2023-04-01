using Core.Entities.Photos;
using Core.Enums;

namespace Core.Entities;

public class Game : BaseEntity<long>
{
    public string? Name { get; set; }
    public Genre Genre { get; set; }
    public Mode Mode { get; set; }
    
    public long? PublisherId { get; set; }
    public Publisher? Publisher { get; set; }

    public IList<PhotoGame>? PhotoList { get; set; }
}