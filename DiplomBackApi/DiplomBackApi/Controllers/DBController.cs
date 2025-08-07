using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Litbase.Controllers;

/// <summary>
/// Контроллер БД и сидинга
/// </summary>
[Route("api/db")]
public class DBController : MyBaseController
{

    public DBController(ApplicationContext context, IMemoryCache memoryCache) : base(context, memoryCache)
    {
    }


    [HttpGet("InitDb")]
    public async Task<ActionResult> UserInitDb(int templateId)
    {
        try
        {
            var user = GetUserIdByAuth();
            await db.UserInitDb(user, templateId);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message + "\n\n" + ex.StackTrace);
        }
        return Ok("Успех");
    }


    [HttpGet("ClearInit")]
    public async Task<ActionResult> ClearInitBd()
    {
        
        try
        {
            await db.ClearInit();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message + "\n\n" + ex.StackTrace);
        }
        return Ok("Успех");
        
    }


    [HttpGet("SaveDb")]
    public async Task<ActionResult> SaveDbToJson()
    {
        
        try
        {
            await db.SaveDbToJson();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message + "\n\n" + ex.StackTrace);
        }
        return Ok("Успех");
        
    }


}
