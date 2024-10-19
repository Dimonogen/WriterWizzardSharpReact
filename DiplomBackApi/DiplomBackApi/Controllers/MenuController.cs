using DiplomBackApi.DTO;
using DiplomBackApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiplomBackApi.Controllers;

/// <summary>
/// Контроллер меню
/// </summary>
[Route("api/menu")]
public class MenuController : MyBaseController
{

    public MenuController(ApplicationContext context) : base(context)
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

        var objs = db.MenuElements.Where(x => x.UserId == user.Id).ToList();//.Where(x => rightIds.Contains(x.RightId)).ToList();

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
        db.SaveChanges();

        return Ok(obj);
        
    }


    /// <summary>
    /// Получение всех пунктов меню
    /// </summary>
    /// <returns></returns>
    [HttpGet("all")]
    public async Task<ActionResult> GetAll()
    {
        var user = GetUserIdByAuth();
        var elems = db.MenuElements.Where(x => x.UserId == user.Id).ToList();

        return Ok(elems);
        
    }


}//END CONTROLLER
