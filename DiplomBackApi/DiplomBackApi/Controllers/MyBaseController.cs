using Litbase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using Microsoft.EntityFrameworkCore;

namespace Litbase.Controllers;

/// <summary>
/// базовый класс контроллера
/// </summary>
public class MyBaseController : ControllerBase
{
    /// <summary>
    /// База данных
    /// </summary>
    protected ApplicationContext db { get; }

    /// <summary>
    /// Кеш в памяти для сокращения доступа к БД
    /// </summary>
    protected IMemoryCache _memoryCache { get; private set; }
    
    /// <summary>
    /// Конструктор класса
    /// </summary>
    /// <param name="context"></param>
    /// <param name="memoryCache"></param>
    public MyBaseController(ApplicationContext context, IMemoryCache memoryCache)
    {
        db = context;
        _memoryCache = memoryCache;
    }

    /// <summary>
    /// Метод для получения пользователя изходя из авторизации
    /// </summary>
    /// <returns></returns>
    protected User GetUserIdByAuth()
    {
        string accessToken = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
        if (accessToken.IsNullOrEmpty())
        {
            throw new Exception("User not found in controller");
        }

        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        JwtSecurityToken jwtSecurityToken = handler.ReadJwtToken(accessToken);
        int id = int.Parse( jwtSecurityToken.Claims.First(c => c.Type == "id").Value );
                    
        if(_memoryCache.TryGetValue(id, out User? cacheUser))
        {
            if (cacheUser == null)
            {
                throw new Exception("User not found in controller");
            }
            return cacheUser;
        }

        var user = db.Users.AsNoTracking().First(x => x.Id == id);
        _memoryCache.Set(id, user);
        return user;
    }
}
