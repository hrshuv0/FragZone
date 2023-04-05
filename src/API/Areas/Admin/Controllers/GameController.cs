using API.Controllers;
using API.Dtos.Game;
using API.Helpers;
using API.Helpers.Pagination;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace API.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class GameController : BaseApiController
{
    #region CONFIG

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private Func<IQueryable<Game>, IIncludableQueryable<Game, object>>? _include;

    public GameController(ILoggerFactory factory, IUnitOfWork unitOfWork, 
        IMapper mapper)
    {
        _logger = factory.CreateLogger<GameController>();
        _unitOfWork = unitOfWork;
        _mapper = mapper;
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
            
            _include = q => q
                .Include(x => x.Category)
                .Include(x => x.Publisher)!;

            (result, total, totalFiltered, totalPages) = await _unitOfWork.GameService.LoadAsync(c => c, null, null, _include, pagination.PageNumber, pagination.PageSize);
            
            Response.AddPagination(pagination.PageNumber, pagination.PageSize, total, totalFiltered, totalPages);
            
            var data = _mapper.Map<IList<GameDto>>(result);
            
            return Ok(data);
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