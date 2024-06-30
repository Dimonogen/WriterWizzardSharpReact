using Microsoft.AspNetCore.Mvc;

namespace DiplomBackApi.Controllers
{
    /// <summary>
    /// Контроллер БД и сидинга
    /// </summary>
    [Route("api/menu")]
    public class DBController : MyBaseController
    {

        [HttpGet("ClearInit")]
        public async Task<ActionResult> ClearInitBd()
        {
            using (ApplicationContext db = new ApplicationContext())
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
        }
    }
}
