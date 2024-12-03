using DiplomBackApi.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace DiplomBackApi.Controllers;

/// <summary>
/// Контроллер для поиска глобального
/// </summary>
[ApiController]
[Route("api/history")]
public class HistoryController : MyBaseController
{

    ILogger _logger;

    public HistoryController(ApplicationContext context, ILogger<HistoryController> logger) : base(context)
    {
        _logger = logger;
    }


    /// <summary>
    /// Подгрузка истории для кастомных случаев
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    [HttpGet("")]
    public async Task<ActionResult> Search(string path)
    {
        _logger.LogDebug(path);
        

        return Ok();
    }
}
