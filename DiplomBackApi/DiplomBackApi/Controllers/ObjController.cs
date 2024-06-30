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
    public class ObjController : ControllerBase
    {


        /// <summary>
        /// End Point для получения объекта с аттрибутами по его ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetObj(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                ObjDto? obj = await db.GetObjDtoAsync(id);

                string jsonString = JsonSerializer.Serialize(obj);

                return Ok(obj);
            }
        }

        [HttpGet("link/{id}")]
        public async Task<ActionResult> GetLinkObj(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var list = new List<ObjDto>();
                db.linkObjs.Where(x => x.ObjParentId == id).ToList().ForEach(e =>
                {
                    list.Add(db.GetObjDtoAsync(e.ObjChildId).Result);
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
                });

                return Ok(list);
            }
        }

        [HttpPost("link/")]
        public async Task<ActionResult> CreateLinkObj(int parentId, int childId)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.linkObjs.Add(new LinkObj
                {
                    ObjParentId = parentId,
                    ObjChildId = childId
                });

                db.SaveChanges();

                return Ok("Ok");
            }
        }


        /// <summary>
        /// End Point для получения списка объектов заданного типа
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        [HttpGet("type/{typeId}")]
        public async Task<ActionResult> GetObjTypeList(int typeId)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var objs = await db.Objs.Where(x => x.TypeId == typeId).ToArrayAsync();
                List<ObjDto?> list = new List<ObjDto?>();

                foreach (var obj in objs)
                {
                    list.Add(
                        await db.GetObjDtoAsync(obj.Id)
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
        }

        /// <summary>
        /// Возвращает все Объекты с аттрибутами
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public async Task<ActionResult> GetAll()
        {
            using(ApplicationContext db = new ApplicationContext())
            {
                var objs = await db.Objs.ToListAsync();

                List<ObjDto?> arr = new List<ObjDto?>();

                foreach (var obj in objs)
                {
                    arr.Add(await db.GetObjDtoAsync(obj.Id));
                }

                return Ok(arr);
            }
        }

        /// <summary>
        /// End Point для создания объекта
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<ActionResult> CreateObj(CreateObjModel obj)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Obj obj_n = new Models.Obj
                {
                    StateId = 1,
                    Name = obj.name,
                    TypeId = obj.TypeId
                };

                db.Objs.Add(obj_n);
                await db.SaveChangesAsync();

                foreach (AttributeAddModel attr in obj.attributes)
                {
                    await db.ObjAttributes.AddAsync(new ObjAttribute
                    {
                        Number = attr.number,
                        ObjId = obj_n.Id,
                        Value = attr.value
                    });
                }

                await db.SaveChangesAsync();

                ObjDto? obj_db = await db.GetObjDtoAsync(obj_n.Id);

                return Ok(obj_db);
            }
        }

        /// <summary>
        /// End Point для создания объекта
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost("edit")]
        public async Task<ActionResult> EditObj(EditObjModel obj)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Obj? obj_n = db.Objs.FirstOrDefault(x => x.Id == obj.id);

                if (obj_n == null)
                    return BadRequest();

                obj_n.Name = obj.name;

                foreach (AttributeAddModel attr in obj.attributes)
                {
                    var attr_exist = db.ObjAttributes.FirstOrDefault(
                        x => x.Number == attr.number && x.ObjId == obj.id);

                    if (attr_exist != null)
                    {
                        attr_exist.Value = attr.value;
                    }
                    else
                    {
                        var attr_type = db.ObjTypeAttributes.FirstOrDefault(
                            x => x.TypeId == obj_n.TypeId && x.Number == attr.number
                            );
                        if (attr_type != null)
                        {
                            await db.ObjAttributes.AddAsync(new ObjAttribute
                            {
                                Number = attr.number,
                                ObjId = obj_n.Id,
                                Value = attr.value
                            });
                        }
                        else
                        {
                            //additional attribute to do
                        }
                    }
                    
                }

                await db.SaveChangesAsync();

                ObjDto? obj_db = await db.GetObjDtoAsync(obj_n.Id);

                return Ok(obj_db);
            }
        }


        /// <summary>
        /// Удаляет объект с заданным id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Objs.RemoveRange(db.Objs.Where(x => x.Id == id).ToArray());

                await db.SaveChangesAsync();

                return Ok();
            }
        }

        /// <summary>
        /// Удаляет объекты с заданными id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("deleteList")]
        public async Task<ActionResult> DeleteArray(int[] id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Objs.RemoveRange(db.Objs.Where(x => id.Contains(x.Id)).ToArray());

                await db.SaveChangesAsync();

                return Ok();
            }
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
