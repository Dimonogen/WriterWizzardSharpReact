using DiplomBackApi.DTO;
using Microsoft.AspNetCore.Mvc;

namespace DiplomBackApi.Controllers.UI;

/// <summary>
/// Контроллер UI
/// </summary>
[ApiController]
[Route("api/ui")]
public class UIController : MyBaseController
{

    public UIController(ApplicationContext context) : base(context)
    {
    }

    /// <summary>
    /// API для получения структуры страницы
    /// </summary>
    /// <returns></returns>
    [HttpGet("StartPageStructure")]
    public async Task<ActionResult> GetStartPageStructure()
    {

        return Ok();
    }

    /// <summary>
    /// API для получения структуры страницы
    /// </summary>
    /// <returns></returns>
    [HttpGet("PageStructure")]
    public async Task<ActionResult> GetPageStructure()
    {
        
        return Ok();
    }



}
