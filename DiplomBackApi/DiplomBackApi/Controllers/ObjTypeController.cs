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
    [Route("api/objType")]
    public class ObjTypeController : MyBaseController
    {


        /// <summary>
        /// End Point для создания типа объекта
        /// </summary>
        /// <param name="typeModel"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<ActionResult> CreateObjType(CreateTypeModel typeModel)
        {
            var user = GetUserIdByAuth();
            using (ApplicationContext db = new ApplicationContext())
            {
                ObjType type = new ObjType
                {
                    Name = typeModel.name,
                    Code = typeModel.code,
                    Description = typeModel.description,
                    UserId = user.Id,
                };

                db.ObjTypes.Add(type);

                await db.SaveChangesAsync();

                foreach(var attr in typeModel.attributes)
                {
                    db.ObjTypeAttributes.Add(new ObjTypeAttribute
                    {
                        Name = attr.name,
                        Number = attr.number,
                        TypeId = type.Id,
                        AttributeTypeId = attr.typeId,
                        UserId = user.Id,
                    });
                }

                if (typeModel.createMenu.HasValue && typeModel.createMenu.Value)
                {
                    db.MenuElements.Add(new MenuElement
                    {
                        Name = typeModel.name,
                        Description = typeModel.name,
                        ObjTypeId = type.Id,
                        UserId = user.Id,
                        Filters = ""
                    });
                }

                await db.SaveChangesAsync();

                return Ok(type);
            }
        }

        /// <summary>
        /// End Point для создания типа объекта
        /// </summary>
        /// <param name="typeModel"></param>
        /// <returns></returns>
        [HttpPost("edit")]
        public async Task<ActionResult> EditObjType(EditTypeModel typeModel)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                ObjType? type = db.ObjTypes.FirstOrDefault(x=> x.Id == typeModel.id);

                if (type == null)
                    return BadRequest();

                type.Name = typeModel.name;
                type.Description = typeModel.description;
                type.Code = typeModel.code;

                foreach (var attr in typeModel.attributes)
                {
                    var attr_exist = db.ObjTypeAttributes.FirstOrDefault(
                        x => x.Number == attr.number && x.TypeId == typeModel.id);

                    if (attr_exist == null)
                    {
                        db.ObjTypeAttributes.Add(new ObjTypeAttribute
                        {
                            Name = attr.name,
                            Number = attr.number,
                            TypeId = type.Id,
                            AttributeTypeId = attr.typeId,
                        });
                    }
                    else
                    {
                        attr_exist.Name = attr.name;
                        attr_exist.AttributeTypeId = attr.typeId;
                    }

                    
                }

                await db.SaveChangesAsync();

                var typeDto = new ObjTypeDto
                {
                    Id = type.Id,
                    Name = type.Name,
                    Description = type.Description,
                    attributes = new List<ObjTypeAttributeDto>()
                };

                db.ObjTypeAttributes.Where(x => x.TypeId == type.Id).ToList().ForEach(a =>
                {
                    typeDto.attributes.Add(new ObjTypeAttributeDto
                    {
                        Id = a.Id,
                        Name = a.Name,
                        Number = a.Number,
                        TypeId = a.AttributeTypeId,
                    });
                });

                return Ok(typeDto);
            }
        }

        /// <summary>
        /// Возвращает список всех типов с атрибутами
        /// </summary>
        /// <returns></returns>
        [HttpGet("allInclude")]
        public async Task<ActionResult> GetAllInclude()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<ObjTypeDto> list = new List<ObjTypeDto>();
                db.ObjTypes.ToList().ForEach(type =>
                {
                    var typeDto = new ObjTypeDto
                    {
                        Id = type.Id,
                        Name = type.Name,
                        Code = type.Code,
                        Description = type.Description,
                        attributes = new List<ObjTypeAttributeDto>()
                    };

                    db.ObjTypeAttributes.Where(x => x.TypeId == type.Id).ToList().ForEach(a =>
                    {
                        typeDto.attributes.Add(new ObjTypeAttributeDto
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Number = a.Number,
                            TypeId = a.AttributeTypeId,
                        });
                    });

                    list.Add(typeDto);
                });

                return Ok(list);
            }
        }

        /// <summary>
        /// Возвращает список всех типов без атрибутов
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public async Task<ActionResult> GetAll()
        {
            var user = GetUserIdByAuth();
            using (ApplicationContext db = new ApplicationContext())
            {
                List<ObjTypeDto> list = new List<ObjTypeDto>();
                db.ObjTypes.Where(x=> x.UserId == user.Id).OrderBy(x => x.Id).ToList().ForEach(type =>
                {
                    list.Add(
                        new ObjTypeDto
                        {
                            Id = type.Id,
                            Name = type.Name,
                            Code = type.Code,
                            Description = type.Description
                        } 
                    );
                });

                //Console.WriteLine("GetAllTypes");

                return Ok(list);
            }
        }


        /// <summary>
        /// Удаляет тип с заданным id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.ObjTypes.RemoveRange(db.ObjTypes.Where(x => x.Id == id).ToArray());

                await db.SaveChangesAsync();

                return Ok();
            }
        }


        /// <summary>
        /// Возвращает информацию о типе с атрибутами
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetItem(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var type = await db.ObjTypes.FirstOrDefaultAsync(x => x.Id == id);

                if (type == null)
                    return Ok();

                var typeDto = new ObjTypeDto
                {
                    Id = type.Id,
                    Name = type.Name,
                    Code = type.Code,
                    Description = type.Description,
                    attributes = new List<ObjTypeAttributeDto>()
                };

                db.ObjTypeAttributes.Where(x => x.TypeId == type.Id).ToList().ForEach(a =>
                {
                    var attrType = db.AttributeTypes.FirstOrDefault(x => x.Id == a.AttributeTypeId);
                    typeDto.attributes.Add(new ObjTypeAttributeDto
                    {
                        Id = a.Id,
                        Name = a.Name,
                        Number = a.Number,
                        TypeId = a.AttributeTypeId,
                        RegEx = attrType.RegExValidation,
                        Type = attrType.Type,
                        IsComplex = attrType.IsComplex
                    });
                });

                return Ok(typeDto);
            }
        }

    }


    public class CreateTypeModel
    {
        public string name { get; set; }

        public string description { get; set; }

        public string code { get; set; }

        public List<CreateTypeAttributeModel> attributes {  get; set; }

        public bool? createMenu { get; set; }
    }

    public class EditTypeModel
    {
        public int id { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public string code { get; set; }

        public List<CreateTypeAttributeModel> attributes { get; set; }
    }

    public class CreateTypeAttributeModel
    {
        public string name {  set; get; }
        public int number { get; set; }
        public int typeId { get; set; }
    }
}
