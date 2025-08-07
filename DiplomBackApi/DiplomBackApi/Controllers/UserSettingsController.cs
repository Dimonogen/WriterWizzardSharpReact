using Litbase.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace Litbase.Controllers;

/// <summary>
/// Контроллер настроек пользователя
/// </summary>
[ApiController]
[Route("api/userSettings")]
public class UserSettingsController : MyBaseController
{

    public UserSettingsController(ApplicationContext context, IMemoryCache memoryCache) : base(context, memoryCache)
    {

    }

    /// <summary>
    /// Сохранить настройки
    /// </summary>
    /// <param name="gridCode">код настроект</param>
    /// <param name="settings">сами настройки</param>
    /// <returns></returns>
    [Authorize]
    [HttpPut("save")]
    public async Task<ActionResult> Save(string gridCode, [FromBody] UserSettingModel settings)
    {
        
        var user = GetUserIdByAuth();
        var objs = await db.UserSettings.Where(s => s.UserId == user.Id && s.Code == gridCode).ToListAsync();

        if (objs.Count == 0)
        {
            db.UserSettings.Add(new Models.UserSettings
            {
                Code = gridCode,
                UserId = user.Id,
                Value = settings.settings
            });
        }
        else if (objs.Count == 1)
        {
            var obj = objs.First().Value = settings.settings;
        }
        else
        {
            throw new Exception("UserSettings вернул больше значений, критикал ошипка!");
        }

        await db.SaveChangesAsync();
        return Ok("Ok");
        
    }


    /// <summary>
    /// Попробовать взять настройки, в противном случае вернётся пустая строка
    /// </summary>
    /// <param name="gridCode"></param>
    /// <returns>строка с настрйоками</returns>
    [Authorize]
    [HttpGet("")]
    public async Task<ActionResult> GetSettings(string gridCode)
    {
       
        var user = GetUserIdByAuth();
        var objs = await db.UserSettings.Where(s => s.UserId == user.Id && s.Code == gridCode).ToListAsync();
        if(objs.Count == 1)
        {
            return Ok(objs.First().Value);
        }
        else
        {
            return Ok("");
        }
        
    }
}

public class UserSettingModel
{
    public string settings { get; set; }
}
