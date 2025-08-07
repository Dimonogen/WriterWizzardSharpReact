using Litbase.DTO;
using Litbase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Litbase.Controllers;

/// <summary>
/// Контроллер меню
/// </summary>
[Route("api/menu")]
public class MenuController : MyBaseController
{
    /// <summary>
    /// Конструктор класса для контроллера меню
    /// </summary>
    /// <param name="context"></param>
    public MenuController(ApplicationContext context, IMemoryCache memoryCache) : base(context, memoryCache)
    {
    }

    /// <summary>
    /// Получение всего меню для пользователя с учётом его ролей
    /// </summary>
    /// <returns></returns>
    [HttpGet("")]
    public async Task<ActionResult> GetMenu()
    {
        var user = GetUserIdByAuth();
        //var rolesRaw = db.userRoles.Where(x => x.UserId == user.Id).ToList();

        //if (rolesRaw.Count == 0)
        //{
        //    return Ok(new List<int>());
        //}

        //HashSet<int> roleIds = new HashSet<int>();

        //foreach (var role in rolesRaw)
        //{
        //    roleIds.Add(role.RoleId);
        //}

        //HashSet<int> rightIds = new HashSet<int>();

        //foreach (var roleId in  roleIds)
        //{
        //    var rights = db.rightRoles.Where(x => x.RoleId == roleId).ToList();
        //    foreach (var right in rights)
        //    {
        //        rightIds.Add(right.RightId);
        //    }
        //}

        var objs = await db.MenuElements.Where(x => x.UserId == user.Id).ToListAsync();//.Where(x => rightIds.Contains(x.RightId)).ToList();

        List<MenuElemViewDto> list = new List<MenuElemViewDto>();

        foreach (var obj in objs)
        {
            list.Add(new MenuElemViewDto
            {
                id = obj.Id,
                name = obj.Name,
                description = obj.Description,
                ParentMenuId = obj.ParentMenuId,
                ObjTypeId = obj.ObjTypeId,
                Filters = obj.Filters
            });
        }

        return Ok(list);
        
    }

    /// <summary>
    /// Создание нового пункта меню
    /// </summary>
    /// <returns></returns>
    [HttpPost("create")]
    public async Task<ActionResult> CreateMenuElem([FromBody] MenuElementCreateDto model)
    {
        var user = GetUserIdByAuth();
        var obj = new MenuElement
        {
            Name = model.name,
            Description = model.description,
            ObjTypeId = model.ObjTypeId,
            ParentMenuId = model.ParentMenuId,
            Filters = model.Filters,
            UserId = user.Id,
            //RightId = model.RightId
        };
        db.MenuElements.Add(obj);
        await db.SaveChangesAsync();

        return Ok(obj);
        
    }

    /// <summary>
    /// Редактирование пункта меню
    /// </summary>
    /// <returns></returns>
    [HttpPost("edit")]
    public async Task<ActionResult> EditMenuElem([FromBody] MenuElementEditDto model)
    {
        var user = GetUserIdByAuth();
        var elem = await db.MenuElements.FirstOrDefaultAsync(x => x.Id == model.id && x.UserId == user.Id);
        
        if(elem == null)
        {
            throw new Exception("Error. Id menu elem is not correct.");
        }
        
        elem.Name = model.name;
        await db.SaveChangesAsync();

        return Ok(elem);
    }

    /// <summary>
    /// Удаление пункта меню
    /// </summary>
    /// <returns></returns>
    [HttpDelete("")]
    public async Task<ActionResult> EditMenuElem(int id)
    {
        var user = GetUserIdByAuth();
        var elem = await db.MenuElements.FirstOrDefaultAsync(x => x.Id == id && x.UserId == user.Id);

        if (elem == null)
        {
            throw new Exception("Error. Id menu elem is not correct.");
        }

        db.MenuElements.Remove(elem);
        await db.SaveChangesAsync();

        return Ok();
    }

    /// <summary>
    /// Получение всех пунктов меню
    /// </summary>
    /// <returns></returns>
    [HttpGet("all")]
    public async Task<ActionResult> GetAll()
    {
        var user = GetUserIdByAuth();
        var elems = await db.MenuElements.Where(x => x.UserId == user.Id).ToListAsync();

        return Ok(elems);
    }


}//END CONTROLLER
