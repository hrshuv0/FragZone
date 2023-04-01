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
public class PublisherController : BaseApiController
{
    #region CONFIG

    private readonly IUnitOfWork _unitOfWork;

    public PublisherController(ILoggerFactory factory, IUnitOfWork unitOfWork)
    {
        _logger = factory.CreateLogger<PublisherController>();
        _unitOfWork = unitOfWork;
    }

    #endregion


    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] PaginationParams pagination)
    {
        IList<Publisher> result = new List<Publisher>();
        
        try
        {
            var total = 0;
            var totalFiltered = 0;
            var totalPages = 0;
            
            (result, total, totalFiltered, totalPages) = await _unitOfWork.PublisherService.LoadAsync(c => c, null, null, null, pagination.PageNumber, pagination.PageSize);
            
            Response.AddPagination(pagination.PageNumber, pagination.PageSize, total, totalFiltered, totalPages);
            
            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }

        return BadRequest("Failed To Load Publisher");
    }
    
    [HttpGet("{id:long}")]
    public async Task<IActionResult> Get(long id)
    {
        Publisher result = new Publisher();

        try
        {
            result = await _unitOfWork.PublisherService.GetByIdAsync(id);
            
            if(result is not null)
                return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }

        return NotFound("Publisher Not found");
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(Publisher model)
    {
        try
        {
            var isExists = await _unitOfWork.PublisherService.IsExistsAsync(x => x.Name == model.Name);
            if (isExists)
                return BadRequest($"{model.Name} Already Exists!");

            await _unitOfWork.PublisherService.AddAsync(model);
            await _unitOfWork.SaveChangesAsync();

            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
        return BadRequest("Publisher Creation Failed!");
    }
    
    [HttpPut("edit/{id:long}")]
    public async Task<IActionResult> Edit(long id, Publisher model)
    {
        try
        {
            if (id != model.Id)
                return BadRequest("Something wrong!");

            var dataExists = await _unitOfWork.PublisherService.IsExistsAsync(x => x.Id == id);
            if (!dataExists)
                return NotFound("Publisher Not Found");

            await _unitOfWork.PublisherService.UpdateAsync(model);
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
            var dataExists = await _unitOfWork.PublisherService.IsExistsAsync(x => x.Id == id);

            if (!dataExists)
                return NotFound("Publisher Not Found");

            await _unitOfWork.PublisherService.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
        
        return BadRequest("Publisher Deletion Failed");
    }
}