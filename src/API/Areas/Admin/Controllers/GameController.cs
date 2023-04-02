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
public class GameController : BaseApiController
{
    #region CONFIG

    private readonly IUnitOfWork _unitOfWork;

    public GameController(ILoggerFactory factory, IUnitOfWork unitOfWork)
    {
        _logger = factory.CreateLogger<GameController>();
        _unitOfWork = unitOfWork;
    }

    #endregion


    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] PaginationParams pagination)
    {
        IList<Game> result = new List<Game>();
        
        try
        {
            var total = 0;
            var totalFiltered = 0;
            var totalPages = 0;
            
            (result, total, totalFiltered, totalPages) = await _unitOfWork.GameService.LoadAsync(c => c, null, null, null, pagination.PageNumber, pagination.PageSize);
            
            Response.AddPagination(pagination.PageNumber, pagination.PageSize, total, totalFiltered, totalPages);
            
            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }

        return BadRequest("Failed To Load Games");
    }
    
    [HttpGet("{id:long}")]
    public async Task<IActionResult> Get(long id)
    {
        Game result = new Game();

        try
        {
            result = await _unitOfWork.GameService.GetByIdAsync(id);
            
            if(result is not null)
                return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }

        return NotFound("Game Not found");
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(Game model)
    {
        try
        {
            var isExists = await _unitOfWork.GameService.IsExistsAsync(x => x.Name == model.Name);
            if (isExists)
                return BadRequest($"{model.Name} Already Exists!");

            await _unitOfWork.GameService.AddAsync(model);
            await _unitOfWork.SaveChangesAsync();

            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
        return BadRequest("Game Creation Failed!");
    }
    
    [HttpPut("edit/{id:long}")]
    public async Task<IActionResult> Edit(long id, Game model)
    {
        try
        {
            if (id != model.Id)
                return BadRequest("Something wrong!");

            var dataExists = await _unitOfWork.GameService.IsExistsAsync(x => x.Id == id);
            if (!dataExists)
                return NotFound("Game Not Found");

            await _unitOfWork.GameService.UpdateAsync(model);
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
            var dataExists = await _unitOfWork.GameService.IsExistsAsync(x => x.Id == id);

            if (!dataExists)
                return NotFound("Game Not Found");

            await _unitOfWork.GameService.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
        
        return BadRequest("Game Deletion Failed");
    }
}