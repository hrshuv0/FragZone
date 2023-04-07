using Core.Enums;
using Infrastructure.Utility;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CommonHelperController : BaseApiController
{
    [HttpGet("game-modes")]
    public IActionResult GetGameModes()
    {
        var gameModes =  FragHelper.LoadEnumToValue<Mode>();

        return Ok(gameModes);
    }
}