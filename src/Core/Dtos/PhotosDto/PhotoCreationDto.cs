using Microsoft.AspNetCore.Http;

namespace Core.Dtos.PhotosDto;

public class PhotoCreationDto
{
    public string? Url { get; set; }
    public IFormFile? File { get; set; }
    public string? PublicId { get; set; }
    private DateTime DateAdded { get; }

    public PhotoCreationDto()
    {
        DateAdded = DateTime.Now;
    }
}