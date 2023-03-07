using API.Controllers;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Areas.Admin.Controllers;

public class CategoryManageController : BaseApiController
{
    #region CONFIG

    private readonly IUnitOfWork _unitOfWork;

    public CategoryManageController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    #endregion


    public async Task<IActionResult> Get()
    {
        IList<Category> result = new List<Category>();
        try
        {
            result = await _unitOfWork.CategoryService.GetAsync(c => c);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return Ok(result);
    }
}