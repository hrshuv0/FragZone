using API.Controllers;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class CategoryController : BaseApiController
{
    #region CONFIG

    private readonly IUnitOfWork _unitOfWork;

    public CategoryController(ILoggerFactory factory, IUnitOfWork unitOfWork)
    {
        _logger = factory.CreateLogger<CategoryController>();
        _unitOfWork = unitOfWork;
    }

    #endregion


    [HttpGet]
    public async Task<IActionResult> Get()
    {
        IList<Category> result = new List<Category>();
        try
        {
            result = await _unitOfWork.CategoryService.GetAsync(c => c);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }

        _logger.LogInformation("Get Category List has been called");
        return Ok(result);
    }
}