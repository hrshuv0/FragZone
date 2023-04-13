using API.Controllers;
using API.Extensions;
using API.Seetings;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Core.Dtos.PhotosDto;
using Core.Entities.Photos;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace API.Areas.Player.Controllers;

[Authorize]
[Route("api/[controller]")]
public class PhotosController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
    private Cloudinary _cloudinary;

    public PhotosController(ILoggerFactory factory, IUnitOfWork unitOfWork, IMapper mapper, IOptions<CloudinarySettings> cloudinaryConfig)
    {
        _logger = factory.CreateLogger<PhotosController>();
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _cloudinaryConfig = cloudinaryConfig;
        
        Account acc = new(
            _cloudinaryConfig.Value.CloudName, 
            _cloudinaryConfig.Value.ApiKey, 
            _cloudinaryConfig.Value.ApiSecret
            );
        _cloudinary = new Cloudinary(acc);
    }
    
    [HttpGet("{id}", Name = "GetPhoto")]
    public async Task<IActionResult> GetPhoto(string id)
    {
        try
        {
            var photoFromRepo = await _unitOfWork.UserService.GetPhoto(id);
            var photo = _mapper.Map<PhotoReturnDto>(photoFromRepo);
            
            return Ok(photo);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting photo");
        }
        return BadRequest("Error while getting photo");
    }

    [HttpPost("{userId}")]
    public async Task<IActionResult> AddPhotoForUser(string userId, [FromForm] PhotoCreationDto photoForCreationDto)
    {
        try
        {
            if (HttpContext.User.GetUserId() != userId)
                return Unauthorized("Unauthorized");
        
            var user = await _unitOfWork.UserService.Get(userId);
            if (user == null)
                return NotFound("User not found");

            var file = photoForCreationDto.File;
            var uploadResult = new ImageUploadResult();

            if (file!.Length > 0)
            {
                await using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.Name, stream),
                    Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
                };

                uploadResult = _cloudinary.Upload(uploadParams);
            }

            photoForCreationDto.Url = uploadResult.Uri.ToString();
            photoForCreationDto.PublicId = uploadResult.PublicId;

            var photo = _mapper.Map<Photo>(photoForCreationDto);

            if (!user.Photos!.Any(u => u.IsMain))
                photo.IsMain = true;

            user.Photos!.Add(photo);

            if (await _unitOfWork.SaveAllAsync())
            {
                var photoToReturn = _mapper.Map<PhotoReturnDto>(photo);
                return CreatedAtRoute("GetPhoto", new {id = photo.Id}, photoToReturn);
            }

        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while adding photo");
        }

        return BadRequest("Could not add the photo");
    }
    
    [HttpPost("{id}/setMain/{userId}")]
    public async Task<IActionResult> SetMainPhoto(string id, string userId)
    {
        try
        {
            if (HttpContext.User.GetUserId() != userId)
                return Unauthorized();
        
            var user = await _unitOfWork.UserService.Get(userId);
            if (user == null)
                return NotFound("User not found");

            var photoFromRepo = await _unitOfWork.UserService.GetPhoto(id);
            if (photoFromRepo == null)
                return NotFound("Photo not found");

            if (photoFromRepo.IsMain)
                return BadRequest("This is already the main photo");

            var currentMainPhoto = await _unitOfWork.UserService.GetMainPhotoForUser(userId);
            currentMainPhoto.IsMain = false;
            photoFromRepo.IsMain = true;

            if (await _unitOfWork.SaveAllAsync())
                return NoContent();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while setting main photo");
        }

        return BadRequest("Could not set photo to main");
    }
    
    [HttpDelete("{id}/delete/{userId}")]
    public async Task<IActionResult> DeletePhoto(string id, string userId)
    {
        try
        {
            if (HttpContext.User.GetUserId() != userId)
                return Unauthorized();
        
            var user = await _unitOfWork.UserService.Get(userId);
            if (user == null)
                return NotFound("User not found");

            var photoFromRepo = await _unitOfWork.UserService.GetPhoto(id);
            if (photoFromRepo == null)
                return NotFound("Photo not found");

            if (photoFromRepo.IsMain)
                return BadRequest("You cannot delete your main photo");

            if (photoFromRepo.PublicId != null)
            {
                var deleteParams = new DeletionParams(photoFromRepo.PublicId);
                var result = await _cloudinary.DestroyAsync(deleteParams);

                if (result.Result == "ok")
                    await _unitOfWork.UserService.Delete(photoFromRepo);
            }

            if (photoFromRepo.PublicId == null)
                await _unitOfWork.UserService.Delete(photoFromRepo);

            if (await _unitOfWork.SaveAllAsync())
                return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while deleting photo");
        }

        return BadRequest("Failed to delete the photo");
    }
    
}