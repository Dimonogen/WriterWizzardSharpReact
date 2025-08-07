using Litbase.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Litbase.Controllers.UI;

/// <summary>
/// Контроллер UI
/// </summary>
[ApiController]
[Route("api/ui")]
public class UIController : MyBaseController
{

    public UIController(ApplicationContext context, IMemoryCache memoryCache) : base(context, memoryCache)
    {
    }

    /// <summary>
    /// API для получения структуры страницы
    /// </summary>
    /// <returns></returns>
    [HttpGet("StartPageStructure")]
    public async Task<ActionResult> GetStartPageStructure()
    {
        return Ok("test");
    }

    /// <summary>
    /// API для получения структуры страницы
    /// </summary>
    /// <returns></returns>
    [HttpGet("PageStructure")]
    public async Task<ActionResult> GetPageStructure()
    {
        return Ok("test");
    }
}
