using DiplomBackApi.DTO;
using Microsoft.AspNetCore.Mvc;

namespace DiplomBackApi.Controllers;

/// <summary>
/// Контроллер избранного
/// </summary>
[ApiController]
[Route("api/favorite")]
public class ObjFavoriteController : MyBaseController
{

    public ObjFavoriteController(ApplicationContext context) : base(context)
    {
    }

    /// <summary>
    /// API для получения всех объектов в избранном у данного пользователя
    /// </summary>
    /// <returns></returns>
    [HttpGet("all")]
    public async Task<ActionResult> GetAll()
    {
        
        var user = GetUserIdByAuth();
        var objs = db.FavoriteObjs.Where(x => x.UserId == user.Id).ToList();

        var list = new List<ObjDto>();

        foreach (var obj in objs)
        {
            list.Add(await db.GetObjDtoAsync(obj.ObjId, user));
        }

        return Ok(list);
        
    }

    /// <summary>
    /// Добавить объект в избранное
    /// </summary>
    /// <param name="objId"></param>
    /// <returns></returns>
    [HttpPut("{objId}")]
    public async Task<ActionResult> Add(int objId)
    {
        var user = GetUserIdByAuth();
        var objs = db.FavoriteObjs.Where(x => x.UserId == user.Id && x.ObjId == objId).ToList();
        if(objs.Count == 0)
        {
            db.FavoriteObjs.Add(new Models.FavoriteObj { 
                UserId = user.Id,
                ObjId = objId
            });
            db.SaveChanges();
            return Ok("Ok");
        }
        else
        {
            return BadRequest("Already exist in favorite");
        }
    }

    /// <summary>
    /// Убрать объект из избранного
    /// </summary>
    /// <param name="objId"></param>
    /// <returns></returns>
    [HttpDelete("{objId}")]
    public async Task<ActionResult> Remove(int objId)
    {
        var user = GetUserIdByAuth();
        var objs = db.FavoriteObjs.Where(x => x.UserId == user.Id && x.ObjId == objId).ToList();
        if (objs.Count == 1)
        {
            db.FavoriteObjs.Remove(objs[0]);
            db.SaveChanges();
            return Ok("Ok");
        }
        else
        {
            return BadRequest("Already exist in favorite");
        }
    }
}
