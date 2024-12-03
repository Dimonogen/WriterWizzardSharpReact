using Microsoft.AspNetCore.Mvc;
using DiplomBackApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using DiplomBackApi.DTO;
using System.Security.Cryptography;
using System.Text;

namespace DiplomBackApi.Controllers;

/// <summary>
/// Контроллер для пользователей
/// </summary>
[ApiController]
[Route("api/user")]
public class UserController : MyBaseController
{

    private readonly ILogger<UserController> _logger;

    /// <summary>
    /// Конструктор класса
    /// </summary>
    /// <param name="logger"></param>
    public UserController(ILogger<UserController> logger, ApplicationContext context) : base(context)
    {
        _logger = logger;
    }

    
    /// <summary>
    /// Получение всех прав пользователя
    /// </summary>
    /// <returns></returns>
    //[HttpGet("rights")]
    //public async Task<ActionResult> GetRights()
    //{
    //    using (ApplicationContext db = new ApplicationContext())
    //    {
    //        var user = GetUserIdByAuth();
    //        var rolesRaw = db.userRoles.Where(x => x.UserId == user.Id).ToList();

    //        if (rolesRaw.Count == 0)
    //        {
    //            return Ok(new List<int>());
    //        }

    //        HashSet<int> roleIds = new HashSet<int>();

    //        foreach (var role in rolesRaw)
    //        {
    //            roleIds.Add(role.RoleId);
    //        }

    //        HashSet<int> rightIds = new HashSet<int>();

    //        foreach (var roleId in roleIds)
    //        {
    //            var rights = db.rightRoles.Where(x => x.RoleId == roleId).ToList();
    //            foreach (var right in rights)
    //            {
    //                rightIds.Add(right.RightId);
    //            }
    //        }

    //        var objs = db.rights.Where(x => rightIds.Contains(x.Id)).ToList();
    //        List<string> list = new List<string>();
    //        foreach (var obj in objs)
    //        {
    //            list.Add(obj.Code);
    //        }

    //        return Ok(list);
    //    }
    //}


    /// <summary>
    /// end point для логина
    /// </summary>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginModel loginModel)
    {
        //_logger.LogInformation($"email:{loginModel.email} password:{loginModel.password}");

        var sha = SHA256.Create();

        string emailHash = Convert.ToHexString( sha.ComputeHash(Encoding.Default.GetBytes(loginModel.email)) );
        string passwordHash = Convert.ToHexString( sha.ComputeHash(Encoding.Default.GetBytes(loginModel.password)));

        var user = await db.Users.FirstOrDefaultAsync(
            u => u.Email == emailHash 
            && u.Password == passwordHash
        );

        if ( user == null )
        {
            return BadRequest("{\"error\":\"Ошибка, неверная комбинация логина и пароля.\"}");
        }

        var claims = new List<Claim> {  new Claim("email", user.Email),
                                        new Claim("id", user.Id.ToString()),
                                        new Claim("role", user.Role)
        };

        // создаем JWT-токен
        var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: claims,
                expires: DateTime.UtcNow.Add(AuthOptions.time),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        return Ok("{\"token\": \"" + new JwtSecurityTokenHandler().WriteToken(jwt) + "\"}");
        
    }

    /// <summary>
    /// end point для регистрации
    /// </summary>
    /// <returns></returns>
    [HttpPost("registration")]
    public async Task<ActionResult> Registration([FromBody] RegistrationModel model)
    {
        var sha = SHA256.Create();

        string emailHash = Convert.ToHexString(sha.ComputeHash(Encoding.Default.GetBytes(model.email)));
        string passwordHash = Convert.ToHexString(sha.ComputeHash(Encoding.Default.GetBytes(model.password)));

        var users = await db.Users.Where(
            u => u.Email == emailHash
        ).ToListAsync();

        if (users.Count > 0)
        {
            return BadRequest("{\"error\":\"Пользователь с таким email уже существует. Регистрация невозможна.\"}");
        }

        User user = new User
        {
            Email = emailHash,
            Password = passwordHash,
            Name = model.name,
            ProjectName = model.projectName,
            Description = "",
            Icon = "",
            Role = "",
            Title = "",
            LastAuth = DateTime.UtcNow,
        };

        await db.Users.AddAsync(user);
        await db.SaveChangesAsync();

        //dfefault template now is id = 0
        await db.UserInitDb(user, 0);

        var claims = new List<Claim> {  new Claim("email", user.Email),
                                        new Claim("id", user.Id.ToString()),
                                        new Claim("role", user.Role)
        };

        // создаем JWT-токен
        var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: claims,
                expires: DateTime.UtcNow.Add(AuthOptions.time),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        return Ok("{\"token\": \"" + new JwtSecurityTokenHandler().WriteToken(jwt) + "\"}");
    }

    /// <summary>
    /// Проверка валидности токена авторизации
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpGet("auth")]
    public async Task<ActionResult> AuthTest()
    {
        string userId = HttpContext.User.Claims.FirstOrDefault(u => u.Type == "id").Value;
        int id = int.Parse(userId);
        //Console.WriteLine($"id auth = {userId}");

        var user = await db.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            return Ok("error");
        }

        var claims = new List<Claim> {  new Claim("email", user.Email),
                                        new Claim("id", user.Id.ToString()),
                                        new Claim("role", user.Role)
        };

        // создаем JWT-токен
        var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: claims,
                expires: DateTime.UtcNow.Add(AuthOptions.time),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        return Ok("{\"token\": \"" + new JwtSecurityTokenHandler().WriteToken(jwt) + "\"}");
        
    }

    /// <summary>
    /// Общедоступная информация о пользователе
    /// </summary>
    /// <returns></returns>
    [HttpGet("{id}/info")]
    public async Task<ActionResult<User>> GetUserInfo(int id)
    {
        //Console.WriteLine($"GetUserInfo: id={id}");

        var user = await db.Users.FirstOrDefaultAsync(x => x.Id == id);

        if (user != null)
            return Ok(user);
        else
            return BadRequest($"user with id={id} not found"); 
    }

    /// <summary>
    /// Информация о владельце токена
    /// </summary>
    /// <returns></returns>
    [HttpGet("myInfo")]
    public string GetCurrentUserInfo()
    {
        return "";
    }

}


public class LoginModel
{
    public string email { get; set; }
    public string password { get; set; }
}


public class RegistrationModel
{
    public string projectName { get; set; }
    public string name { get; set; }
    public string email { get; set; }
    public string password { get; set; }
}