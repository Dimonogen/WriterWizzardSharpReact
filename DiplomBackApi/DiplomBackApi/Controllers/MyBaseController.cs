using DiplomBackApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;

namespace DiplomBackApi.Controllers
{
    /// <summary>
    /// базовый класс контроллера
    /// </summary>
    public class MyBaseController : ControllerBase
    {
        /// <summary>
        /// Метод для получения пользователя изходя из авторизации
        /// </summary>
        /// <returns></returns>
        protected User GetUserIdByAuth()
        {
            string accessToken = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            if (accessToken.IsNullOrEmpty())
            {
                return null;
            }

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtSecurityToken = handler.ReadJwtToken(accessToken);
            int id = int.Parse( jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == "id")?.Value );

            using (ApplicationContext db = new ApplicationContext())
            {
                var user = db.Users.FirstOrDefault(x => x.Id == id);
                return user;
            }
        }
    }
}
