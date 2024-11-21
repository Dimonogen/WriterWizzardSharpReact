using DiplomBackApi.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace DiplomBackApi.Controllers;

/// <summary>
/// Контроллер для поиска глобального
/// </summary>
[ApiController]
[Route("api/search")]
public class SearchController : MyBaseController
{

    public SearchController(ApplicationContext context) : base(context)
    {

    }


    /// <summary>
    /// Поиск текста по всем объектам
    /// </summary>
    /// <param name="text"></param>
    /// <returns>строка с настрйоками</returns>
    [HttpGet("")]
    public async Task<ActionResult> Search(string text)
    {
       
        var user = GetUserIdByAuth();
        var objs = await db.ObjAttributes.Where(x => x.Value.Contains(text)).ToListAsync();
        


        var res = new List<ObjDto>();

        return Ok(res);
    }
}
