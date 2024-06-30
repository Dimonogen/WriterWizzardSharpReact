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
    /// ���������� ��� �������������
    /// </summary>
    [ApiController]
    [Route("api/user")]
    public class UserController : MyBaseController
    {

        private readonly ILogger<UserController> _logger;

        /// <summary>
        /// ����������� ������
        /// </summary>
        /// <param name="logger"></param>
        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// ��������� ���� ���� ������������
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
        /// end point ��� ������
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

                // ������� JWT-�����
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
        /// ���� end point ��� �����������
        /// </summary>
        /// <returns></returns>
        [HttpPost("registration")]
        public ActionResult Registration()
        {
            return Ok("Registration");
        }

        /// <summary>
        /// ��������� ��������, ����� ������������
        /// </summary>
        /// <returns></returns>
        [HttpPost("editMyInfo")]
        public string EditUserInfo()
        {
            return "";
        }

        /// <summary>
        /// �������� �������� ������������
        /// </summary>
        /// <returns></returns>
        [HttpPost("logo/update")]
        public string LogoUpdate()
        {
            return "";
        }

        /// <summary>
        /// �������� �������� ������������
        /// </summary>
        /// <returns></returns>
        [HttpPost("logo/delete")]
        public string LogoDelete()
        {
            return "";
        }

        /// <summary>
        /// �������� ���������� ������ �����������
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

                // ������� JWT-�����
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
        /// ������������� ���������� � ������������
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
        /// ���������� � ��������� ������
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
                // �������� ������� �� �� � ������� �� �������
                var users = db.Users.ToList();
                Console.WriteLine("������ ��������:");
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
                // ��������� �� � ��
                db.Users.Add(u);
                db.SaveChanges();
                Console.WriteLine("������� ������� ���������");
            }
        }
    }


    public class LoginModel
    {
        public string email { get; set; }
        public string password { get; set; }
    }
}
