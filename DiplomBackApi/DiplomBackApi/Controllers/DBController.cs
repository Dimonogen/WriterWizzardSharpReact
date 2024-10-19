using Microsoft.AspNetCore.Mvc;

namespace DiplomBackApi.Controllers;

/// <summary>
/// Контроллер БД и сидинга
/// </summary>
[Route("api/menu")]
public class DBController : MyBaseController
{

    public DBController(ApplicationContext context) : base(context)
    {
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
