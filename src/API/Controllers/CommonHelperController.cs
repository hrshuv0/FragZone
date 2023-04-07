using Core.Enums;
using Infrastructure.Utility;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CommonHelperController : BaseApiController
{
    [HttpGet("game-mode")]
    public IActionResult GetGameModes()
    {
        var gameModes =  FragHelper.LoadEnumToDictionary<Mode>();

        return Ok(gameModes);
    }
}