using API.Controllers;
using API.Helpers;
using API.Helpers.Pagination;
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
    public async Task<IActionResult> Get([FromQuery] PaginationParams pagination)
    {
        IList<Category> result = new List<Category>();
        
        try
        {
            var total = 0;
            var totalFiltered = 0;
            var totalPages = 0;
            
            (result, total, totalFiltered, totalPages) = await _unitOfWork.CategoryService.LoadAsync(c => c, null, null, null, pagination.PageNumber, pagination.PageSize);
            
            Response.AddPagination(pagination.PageNumber, pagination.PageSize, total, totalFiltered, totalPages);
            
            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }

        return BadRequest("Failed To Load Category");
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

    [HttpPost("create")]
    public async Task<IActionResult> Create(Category model)
    {
        try
        {
            var isExists = await _unitOfWork.CategoryService.IsExistsAsync(x => x.Name == model.Name);
            if (isExists)
                return BadRequest($"{model.Name} Already Exists!");

            await _unitOfWork.CategoryService.AddAsync(model);
            await _unitOfWork.SaveChangesAsync();

            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
        return BadRequest("Category Creation Failed!");
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

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
        
        return BadRequest("Category Deletion Failed");
    }
}