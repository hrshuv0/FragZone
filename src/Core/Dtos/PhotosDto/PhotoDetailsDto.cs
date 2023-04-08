namespace Core.Dtos.PhotosDto;

public class PhotoDetailsDto
{
    public Guid Id { get; set; }
    public string? Url { get; set; }
    public bool IsMain { get; set; }
    public DateTime DateAdded { get; set; }
}