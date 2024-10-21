using DiplomBackApi.DTO;
using DiplomBackApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace DiplomBackApi.Controllers
{
    /// <summary>
    /// Контроллер api для объектов
    /// </summary>
    [ApiController]
    [Route("api/obj")]
    public class ObjController : MyBaseController
    {

        public ObjController(ApplicationContext context) : base (context)
        {
        }

        /// <summary>
        /// End Point для получения объекта с аттрибутами по его ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetObj(int id)
        {
            var user = GetUserIdByAuth();
            ObjDto? obj = await db.GetObjDtoAsync(id, user);

            string jsonString = JsonSerializer.Serialize(obj);

            return Ok(obj);
            
        }

        [HttpGet("link/{id}")]
        public async Task<ActionResult> GetLinkObj(int id)
        {
            var user = GetUserIdByAuth();
            var list = new List<ObjDto>();
            db.LinkObjs.Where(x => x.ObjParentId == id && x.UserId == user.Id).ToList().ForEach(e =>
            {
                var res = db.GetObjDtoAsync(e.ObjChildId, user).Result;
                if (res != null)
                {
                    list.Add(res);

                    foreach (var attr in list.Last().attributes)
                    {
                        if (attr.IsComplexType && attr.Value != null)
                        {
                            int id;
                            try { id = int.Parse(attr.Value); }
                            catch (Exception ex) { continue; }
                            attr.Value = db.Objs.First(x => x.Id == id).Name;
                        }
                    }
                }
            });

            return Ok(list);
            
        }

        [HttpPost("link/")]
        public async Task<ActionResult> CreateLinkObj(int parentId, int childId)
        {
            var user = GetUserIdByAuth();
            db.LinkObjs.Add(new LinkObj
            {
                ObjParentId = parentId,
                ObjChildId = childId,
                UserId = user.Id
            });

            db.SaveChanges();

            return Ok("Ok");
            
        }


        /// <summary>
        /// End Point для получения списка объектов заданного типа
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        [HttpGet("type/{typeId}")]
        public async Task<ActionResult> GetObjTypeList(int typeId)
        {
            
            var user = GetUserIdByAuth();
            var stateDelete = await db.ObjStates.FirstOrDefaultAsync(x => x.Code == "deleted");
                
            if (stateDelete == null)
            {
                throw new Exception("Not found deleted state in DB");
            }

            var objs = await db.Objs.Where(x => x.TypeId == typeId 
                                        && x.StateId != stateDelete.Id 
                                        && x.UserId == user.Id
                        ).ToArrayAsync();
                
            List<ObjDto?> list = new List<ObjDto?>();

            foreach (var obj in objs)
            {
                list.Add(
                    await db.GetObjDtoAsync(obj.Id, user)
                );
                foreach(var attr in list.Last().attributes)
                {
                    if(attr.IsComplexType && attr.Value != null)
                    {
                        int id;
                        try { id = int.Parse(attr.Value); }
                        catch (Exception e) { continue; }
                        attr.Value = db.Objs.First(x => x.Id == id ).Name;
                    }
                }
            }

            return Ok(list);
            
        }

        /// <summary>
        /// Возвращает все Объекты с аттрибутами
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public async Task<ActionResult> GetAll()
        {
            var user = GetUserIdByAuth();
            var stateDelete = await db.ObjStates.FirstOrDefaultAsync(x => x.Code == "deleted");
            if (stateDelete == null)
                return BadRequest("state deleted not found");

            var objs = await db.Objs.Where(x => x.StateId != stateDelete.Id
                                           && x.UserId == user.Id).ToListAsync();

            List<ObjDto?> arr = new List<ObjDto?>();

            foreach (var obj in objs)
            {
                arr.Add(await db.GetObjDtoAsync(obj.Id, user));
            }

            return Ok(arr);
            
        }


        /// <summary>
        /// Возвращает все Объекты с аттрибутами
        /// </summary>
        /// <returns></returns>
        [HttpGet("treshCan")]
        public async Task<ActionResult> GetTreshCan()
        {
            
            var user = GetUserIdByAuth();
            var stateDelete = await db.ObjStates.FirstOrDefaultAsync(x => x.Code == "deleted");
            if (stateDelete == null)
                return BadRequest("state deleted not found");

            var objs = await db.Objs.Where(x => x.StateId == stateDelete.Id
                        && x.UserId == user.Id).ToListAsync();

            List<ObjDto?> arr = new List<ObjDto?>();

            foreach (var obj in objs)
            {
                arr.Add(await db.GetObjDtoAsync(obj.Id, user));
            }

            return Ok(arr);
            
        }

        /// <summary>
        /// End Point для создания объекта
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<ActionResult> CreateObj(CreateObjModel obj)
        {
            var user = GetUserIdByAuth();

            int countObj = db.Objs.Where(x => x.UserId == user.Id).Count();
            int maxIdObj = 0;
            if (countObj > 0)
                maxIdObj = db.Objs.Where(x => x.UserId == user.Id).Max(x => x.Id);
            
            Obj obj_n = new Models.Obj
            {
                Id = maxIdObj + 1,
                StateId = 1,
                Name = obj.name,
                TypeId = obj.TypeId,
                UserId = user.Id,
            };

            db.Objs.Add(obj_n);
            await db.SaveChangesAsync();

            int count = db.ObjAttributes.Where(x => x.UserId == user.Id).Count();
            int maxId = 0;
            if (count > 0)
                maxId = db.ObjAttributes.Where(x => x.UserId == user.Id).Max(x => x.Id);

            foreach (AttributeAddModel attr in obj.attributes)
            {
                await db.ObjAttributes.AddAsync(new ObjAttribute
                {
                    Id = maxId + 1,
                    Number = attr.number,
                    ObjId = obj_n.Id,
                    Value = attr.value,
                    UserId = user.Id,
                });
                maxId++;
            }

            await db.SaveChangesAsync();

            ObjDto? obj_db = await db.GetObjDtoAsync(obj_n.Id, user);

            return Ok(obj_db);
            
        }

        /// <summary>
        /// End Point для создания объекта
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost("edit")]
        public async Task<ActionResult> EditObj(EditObjModel obj)
        {
            var user = GetUserIdByAuth();
            Obj? obj_n = db.Objs.FirstOrDefault(x => x.Id == obj.id && x.UserId == user.Id);

            if (obj_n == null)
                return BadRequest();

            obj_n.Name = obj.name;

            int count = db.ObjAttributes.Where(x => x.UserId == user.Id).Count();
            int maxId = 0;
            if (count > 0)
                maxId = db.ObjAttributes.Where(x => x.UserId == user.Id).Max(x => x.Id);

            foreach (AttributeAddModel attr in obj.attributes)
            {
                var attr_exist = db.ObjAttributes.FirstOrDefault(
                    x => x.Number == attr.number && x.ObjId == obj.id && x.UserId == user.Id);

                if (attr_exist != null)
                {
                    attr_exist.Value = attr.value;
                }
                else
                {
                    var attr_type = db.ObjTypeAttributes.FirstOrDefault(
                        x => x.TypeId == obj_n.TypeId && x.Number == attr.number
                        && x.UserId == user.Id
                        );
                    if (attr_type != null)
                    {
                        await db.ObjAttributes.AddAsync(new ObjAttribute
                        {
                            Id = maxId + 1,
                            Number = attr.number,
                            ObjId = obj_n.Id,
                            Value = attr.value,
                            UserId = user.Id
                        });
                        maxId = maxId + 1;
                    }
                    else
                    {
                        //additional attribute to do
                    }
                }
                    
            }

            await db.SaveChangesAsync();

            ObjDto? obj_db = await db.GetObjDtoAsync(obj_n.Id, user);

            return Ok(obj_db);
            
        }


        /// <summary>
        /// Удаляет объект с заданным id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(int id)
        {

            //db.Objs.RemoveRange(db.Objs.Where(x => x.Id == id).ToArray());
            var user = GetUserIdByAuth();
            var objs = await db.Objs.Where(x => x.Id == id && x.UserId == user.Id).ToListAsync();
            var state = await db.ObjStates.FirstOrDefaultAsync(x => x.Code == "deleted");

            if(state == null)
            {
                throw new Exception("Not found deleted state in DB");
            }

            foreach(var obj in objs)
            {
                obj.StateId = state.Id;
            }

            await db.SaveChangesAsync();

            return Ok();
            
        }

        /// <summary>
        /// Восстанавливает из корзины объект с заданным id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("restore")]
        public async Task<ActionResult> RestoreItem(int id)
        {
            var user = GetUserIdByAuth();
            var objs = await db.Objs.Where(x => x.Id == id && x.UserId == user.Id).ToListAsync();
            var state = await db.ObjStates.FirstOrDefaultAsync(x => x.Code == "use");

            if (state == null)
            {
                throw new Exception("Not found use state in DB");
            }

            foreach (var obj in objs)
            {
                obj.StateId = state.Id;
            }

            await db.SaveChangesAsync();

            return Ok();
            
        }

        /// <summary>
        /// Удаляет объекты с заданными id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("deleteList")]
        public async Task<ActionResult> DeleteArray(int[] id)
        {

            //db.Objs.RemoveRange(db.Objs.Where(x => id.Contains(x.Id)).ToArray());
            var user = GetUserIdByAuth();
            var objs = await db.Objs.Where(x => id.Contains(x.Id) && x.UserId == user.Id ).ToListAsync();
            var state = await db.ObjStates.FirstOrDefaultAsync(x => x.Code == "deleted");

            if (state == null)
            {
                throw new Exception("Not found deleted state in DB");
            }

            foreach (var obj in objs)
            {
                obj.StateId = state.Id;
            }

            await db.SaveChangesAsync();

            return Ok();
            
        }


        /// <summary>
        /// Восстанавливает из корзины объекты с заданными id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("restoreList")]
        public async Task<ActionResult> RestoreArray(int[] id)
        {
            var user = GetUserIdByAuth();
            var objs = await db.Objs.Where(x => id.Contains(x.Id) && x.UserId == user.Id).ToListAsync();
            var state = await db.ObjStates.FirstOrDefaultAsync(x => x.Code == "use");

            if (state == null)
            {
                throw new Exception("Not found use state in DB");
            }

            foreach (var obj in objs)
            {
                obj.StateId = state.Id;
            }

            await db.SaveChangesAsync();

            return Ok();
            
        }


        /// <summary>
        /// Получение всех состояний объектов
        /// </summary>
        /// <returns></returns>
        [HttpGet("states")]
        public async Task<ActionResult> GetAllStates()
        {
            var user = GetUserIdByAuth();
            var elems = db.ObjStates.Where(x => x.UserId == user.Id).ToList();

            return Ok(elems);

        }

    }


    public class AttributeAddModel
    {
        public string value { get; set; }
        public int number { get; set; }
    }


    public class CreateObjModel
    {
        public string name { get; set; }
        public int TypeId { get; set; }

        public List<AttributeAddModel> attributes { get; set; }
    }

    public class EditObjModel
    {
        public int id { get; set; }
        public string name { get; set; }

        public List<AttributeAddModel> attributes { get; set; }
    }
}
