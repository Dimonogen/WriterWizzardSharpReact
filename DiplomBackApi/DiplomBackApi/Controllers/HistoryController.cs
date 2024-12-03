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
        List<string> res = new List<string>();
        string[] segments = path.Split('/',StringSplitOptions.RemoveEmptyEntries);

        var user = GetUserIdByAuth();

        //_logger.LogDebug(path);
        if(path.Contains("menu"))
        {
            if(segments.Length > 1)
            {
                var objTypes = db.ObjTypes.Where(x => x.Id == int.Parse(segments[1]) && x.UserId == user.Id).Single();
                res.Add(objTypes.Name);
            }
            if (segments.Length > 2)
            {
                var obj = db.Objs.Where(x => x.Id == int.Parse(segments[2]) && x.UserId == user.Id).Single();
                res.Add(obj.Name);
            }
        }

        if(path.Contains("config"))
        {
            if (segments.Length > 1)
            {
                string sRes = "";
                switch(segments[1])
                {
                    case "1":
                        sRes = "Типы объектов";
                        break;
                    case "2":
                        sRes = "Типы атрибутов";
                        break;
                    case "3":
                        sRes = "Пункты меню";
                        break;
                    case "4":
                        sRes = "Состояния объектов";
                        break;
                }
                res.Add(sRes);
            }
            if (segments.Length > 2)
            {
                switch (segments[1])
                {
                    case "1":
                        //sRes = "Типы объектов";
                        var objTyp = db.ObjTypes.Where(x => x.Id == int.Parse(segments[2]) && x.UserId == user.Id).Single();
                        res.Add(objTyp.Name);
                        break;
                    case "2":
                        //sRes = "Типы атрибутов";
                        var attrType = db.AttributeTypes.Where(x => x.Id == int.Parse(segments[2]) && x.UserId == user.Id).Single();
                        res.Add(attrType.Name);
                        break;
                    case "3":
                        //sRes = "Пункты меню";
                        var menu = db.MenuElements.Where(x => x.Id == int.Parse(segments[2]) && x.UserId == user.Id).Single();
                        res.Add(menu.Name);
                        break;
                    case "4":
                        //sRes = "Состояния объектов";
                        var obj = db.ObjStates.Where(x => x.Id == int.Parse(segments[2]) && x.UserId == user.Id).Single();
                        res.Add(obj.Name);
                        break;
                }
                
            }
        }

        return Ok(res);
    }
}
