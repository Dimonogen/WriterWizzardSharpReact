using Microsoft.AspNetCore.Mvc;
using DiplomBackApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using DiplomBackApi.DTO;

namespace DiplomBackApi.Controllers
{
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
        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Получение всех прав пользователя
        /// </summary>
        /// <returns></returns>
        [HttpGet("rights")]
        public async Task<ActionResult> GetRights()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var user = GetUserIdByAuth();
                var rolesRaw = db.userRoles.Where(x => x.UserId == user.Id).ToList();

                if (rolesRaw.Count == 0)
                {
                    return Ok(new List<int>());
                }

                HashSet<int> roleIds = new HashSet<int>();

                foreach (var role in rolesRaw)
                {
                    roleIds.Add(role.RoleId);
                }

                HashSet<int> rightIds = new HashSet<int>();

                foreach (var roleId in roleIds)
                {
                    var rights = db.rightRoles.Where(x => x.RoleId == roleId).ToList();
                    foreach (var right in rights)
                    {
                        rightIds.Add(right.RightId);
                    }
                }

                var objs = db.rights.Where(x => rightIds.Contains(x.Id)).ToList();
                List<string> list = new List<string>();
                foreach (var obj in objs)
                {
                    list.Add(obj.Code);
                }

                return Ok(list);
            }
        }


        /// <summary>
        /// end point для логина
        /// </summary>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginModel loginModel)
        {
            _logger.LogInformation($"email:{loginModel.email} password:{loginModel.password}");
            using (ApplicationContext db = new ApplicationContext())
            {
                var user = await db.Users.FirstOrDefaultAsync(
                    u => u.Email == loginModel.email 
                    && u.Password == loginModel.password
                );

                if ( user == null )
                {
                    return Ok("{\"error\":\"error\"}");
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
        }

        /// <summary>
        /// Типо end point для регистрации
        /// </summary>
        /// <returns></returns>
        [HttpPost("registration")]
        public ActionResult Registration()
        {
            return Ok("Registration");
        }

        /// <summary>
        /// Изменение описания, имени пользователя
        /// </summary>
        /// <returns></returns>
        [HttpPost("editMyInfo")]
        public string EditUserInfo()
        {
            return "";
        }

        /// <summary>
        /// Загрузка аватарки пользователя
        /// </summary>
        /// <returns></returns>
        [HttpPost("logo/update")]
        public string LogoUpdate()
        {
            return "";
        }

        /// <summary>
        /// Удаление аватарки пользователя
        /// </summary>
        /// <returns></returns>
        [HttpPost("logo/delete")]
        public string LogoDelete()
        {
            return "";
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
            Console.WriteLine($"id auth = {userId}");

            using (ApplicationContext db = new ApplicationContext())
            {
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
        }

        /// <summary>
        /// Общедоступная информация о пользователе
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}/info")]
        public async Task<ActionResult<User>> GetUserInfo(int id)
        {
            Console.WriteLine($"GetUserInfo: id={id}");

            using (ApplicationContext db = new ApplicationContext())
            {
                var user = await db.Users.FirstOrDefaultAsync(x => x.Id == id);

                if (user != null)
                    return Ok(user);
                else
                    return BadRequest($"user with id={id} not found");
            }   
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

        /// <summary>
        /// DEBUG TO DELETE
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUsers")]
        public IEnumerable<User> Get()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                // получаем объекты из бд и выводим на консоль
                var users = db.Users.ToList();
                Console.WriteLine("Список объектов:");
                foreach (User u in users)
                {
                    Console.WriteLine($"{u.Id}.{u.Name}");
                }

                return users;
            }
        }

        /// <summary>
        /// DEBUG TO DELETE
        /// </summary>
        /// <returns></returns>
        [HttpPost("AddUser")]
        public void Add(User u)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                // добавляем их в бд
                db.Users.Add(u);
                db.SaveChanges();
                Console.WriteLine("Объекты успешно сохранены");
            }
        }
    }


    public class LoginModel
    {
        public string email { get; set; }
        public string password { get; set; }
    }
}
