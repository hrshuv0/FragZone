using API.Helpers.Pagination;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class FragUserController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;

    public FragUserController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    [HttpGet("users")]
    public async Task<IActionResult> GetUsers([FromQuery] PaginationParams paginationParams)
    {
        try
        {
            var result = await _unitOfWork.UserService.Load(paginationParams.PageNumber, paginationParams.PageSize);

            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting users");
        }

        return BadRequest("Error while getting users");
    }

}