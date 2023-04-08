using API.Helpers.Pagination;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;


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
        var result = await _unitOfWork.UserService.Load(paginationParams.PageNumber, paginationParams.PageSize);

        return Ok(result);
    }

}