namespace Core.Dtos.PhotosDto;

public class PhotoReturnDto
{
    public string? Id { get; set; }
    public string? Url { get; set; }
    public string? PublicId { get; set; }
    private DateTime DateAdded { get; }
    private bool IsMain { get; }
}