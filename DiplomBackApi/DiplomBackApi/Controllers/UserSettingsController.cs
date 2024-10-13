using DiplomBackApi.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace DiplomBackApi.Controllers;

/// <summary>
/// Контроллер настроек пользователя
/// </summary>
[ApiController]
[Route("api/userSettings")]
public class UserSettingsController : MyBaseController
{

    /// <summary>
    /// Сохранить настройки
    /// </summary>
    /// <param name="gridCode">код настроект</param>
    /// <param name="settings">сами настройки</param>
    /// <returns></returns>
    [HttpPut("save")]
    public async Task<ActionResult> Save(string gridCode, [FromBody] UserSettingModel settings)
    {
        using (ApplicationContext db = new ApplicationContext())
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
    }


    /// <summary>
    /// Попробовать взять настройки, в противном случае вернётся пустая строка
    /// </summary>
    /// <param name="gridCode"></param>
    /// <returns>строка с настрйоками</returns>
    [HttpGet("")]
    public async Task<ActionResult> GetSettings(string gridCode)
    {
        using (ApplicationContext db = new ApplicationContext())
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
}

public class UserSettingModel
{
    public string settings { get; set; }
}
