using DiplomBackApi.DTO;
using DiplomBackApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiplomBackApi.Controllers
{
    /// <summary>
    /// Контроллер меню
    /// </summary>
    [Route("api/menu")]
    public class MenuController : MyBaseController
    {
        /// <summary>
        /// Получение всего меню для пользователя с учётом его ролей
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        public async Task<ActionResult> GetMenu()
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

                foreach (var roleId in  roleIds)
                {
                    var rights = db.rightRoles.Where(x => x.RoleId == roleId).ToList();
                    foreach (var right in rights)
                    {
                        rightIds.Add(right.RightId);
                    }
                }

                var objs = db.menuElements.Where(x => rightIds.Contains(x.RightId)).ToList();

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
        }

        /// <summary>
        /// Создание нового пункта меню
        /// </summary>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<ActionResult> CreateMenuElem([FromBody] MenuElementCreateDto model)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var obj = new MenuElement
                {
                    Name = model.name,
                    Description = model.description,
                    ObjTypeId = model.ObjTypeId,
                    ParentMenuId = model.ParentMenuId,
                    Filters = model.Filters,
                    RightId = model.RightId
                };
                db.menuElements.Add(obj);
                db.SaveChanges();

                return Ok(obj);
            }
        }


        /// <summary>
        /// Получение всех пунктов меню
        /// </summary>
        /// <returns></returns>
        [HttpPost("all")]
        public async Task<ActionResult> GetAll()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var elems = db.menuElements.ToList();

                return Ok(elems);
            }
        }


    }//END CONTROLLER
}
