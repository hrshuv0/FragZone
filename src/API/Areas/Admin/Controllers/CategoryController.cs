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
    
    [HttpGet("{id:long}")]
    public async Task<IActionResult> Get(long id)
    {
        Category category = new Category();

        try
        {
            category = await _unitOfWork.CategoryService.GetByIdAsync(id);
            
            if(category is not null)
                return Ok(category);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }

        return NotFound("Category Not found");
    }

    [HttpPut("edit/{id:long}")]
    public async Task<IActionResult> Edit(long id, Category model)
    {
        try
        {
            if (id != model.Id)
                return BadRequest("Something wrong!");

            var categoryExists = await _unitOfWork.CategoryService.IsExistsAsync(x => x.Id == id);
            if (!categoryExists)
                return NotFound("Category Not Found");

            await _unitOfWork.CategoryService.UpdateAsync(model);
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }


        return Ok();
    }

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        try
        {
            var categoryExists = await _unitOfWork.CategoryService.IsExistsAsync(x => x.Id == id);

            if (!categoryExists)
                return NotFound("Category Not Found");

            await _unitOfWork.CategoryService.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();

            return Ok("Category Deleted Successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
        
        return BadRequest("Category Deletion Failed");
    }
}