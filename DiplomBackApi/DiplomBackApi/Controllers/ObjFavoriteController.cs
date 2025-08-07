using Litbase.DTO;
using Litbase.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Litbase.Controllers;

/// <summary>
/// Контроллер избранного
/// </summary>
[ApiController]
[Route("api/favorite")]
public class ObjFavoriteController : MyBaseController
{
    /// <summary>
    /// Сервис для объектов
    /// </summary>
    protected ObjectsService _objectsService { get; set; }

    public ObjFavoriteController(ApplicationContext context, IMemoryCache memoryCache, ObjectsService objectsService) : base(context, memoryCache)
    {
        _objectsService = objectsService;
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
            list.Add(await _objectsService.GetObjDtoAsync(obj.ObjId, user.Id));
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
