using Litbase.DTO;
using Litbase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace Litbase.Controllers;

/// <summary>
/// Контроллер api для объектов
/// </summary>
[ApiController]
[Authorize]
[Route("api/objType")]
public class ObjTypeController : MyBaseController
{

    public ObjTypeController(ApplicationContext context, IMemoryCache memoryCache) : base(context, memoryCache)
    { }

    /// <summary>
    /// End Point для создания типа объекта
    /// </summary>
    /// <param name="typeModel"></param>
    /// <returns></returns>

    [HttpPost("create")]
    public async Task<ActionResult> CreateObjType(CreateTypeModel typeModel)
    {
        var user = GetUserIdByAuth();

        int maxId = db.ObjTypes.Where(x => x.UserId == user.Id).Max(x => x.Id);

        ObjType type = new ObjType
        {
            Id = maxId + 1,
            Name = typeModel.name,
            Code = typeModel.code,
            Description = typeModel.description,
            UserId = user.Id,
        };

        db.ObjTypes.Add(type);

        await db.SaveChangesAsync();

        foreach (var attr in typeModel.attributes)
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

        //Это когда мы из меню создаём новый пункт меню
        if (typeModel.createMenu.HasValue && typeModel.createMenu.Value)
        {
            int maxIdMenu = db.MenuElements.Where(x => x.UserId == user.Id).Max(x => x.Id);
            db.MenuElements.Add(new MenuElement
            {
                Id = maxIdMenu + 1,
                Name = typeModel.name,
                Description = typeModel.name,
                ObjTypeId = type.Id,
                UserId = user.Id,
                Filters = ""
            });

            int maxIdTypeAttr = db.AttributeTypes.Where(x => x.UserId == user.Id).Max(x => x.Id);
            db.AttributeTypes.Add(new AttributeType
            {
                Id = maxIdTypeAttr + 1,
                Name = "Ссылка " + typeModel.name,
                IsComplex = true,
                RegExValidation = "",
                Type = type.Id,
                UserId = user.Id,
            });
        }

        await db.SaveChangesAsync();

        return Ok(type);

    }

    /// <summary>
    /// End Point для создания типа объекта
    /// </summary>
    /// <param name="typeModel"></param>
    /// <returns></returns>
    [HttpPost("edit")]
    public async Task<ActionResult> EditObjType(EditTypeModel typeModel)
    {
        var user = GetUserIdByAuth();
        ObjType? type = db.ObjTypes.FirstOrDefault(x => x.Id == typeModel.id && x.UserId == user.Id);

        if (type == null)
            return BadRequest();

        type.Name = typeModel.name;
        type.Description = typeModel.description;
        type.Code = typeModel.code;

        int count = db.ObjTypeAttributes.Where(x => x.UserId == user.Id).Count();
        int maxId = 0;
        if (count > 0)
            maxId = db.ObjTypeAttributes.Where(x => x.UserId == user.Id).Max(x => x.Id);

        foreach (var attr in typeModel.attributes)
        {
            var attr_exist = db.ObjTypeAttributes.FirstOrDefault(
                x => x.Number == attr.number && x.TypeId == typeModel.id && x.UserId == user.Id);

            if (attr_exist == null)
            {
                db.ObjTypeAttributes.Add(new ObjTypeAttribute
                {
                    Id = maxId + 1,
                    Name = attr.name,
                    Number = attr.number,
                    TypeId = type.Id,
                    AttributeTypeId = attr.typeId,
                    UserId = user.Id,
                });
                maxId = maxId + 1;
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
            attributes = new List<ObjTypeAttributeDto>(),
        };

        db.ObjTypeAttributes.Where(x => x.TypeId == type.Id && x.UserId == user.Id).ToList().ForEach(a =>
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

    /// <summary>
    /// Возвращает список всех типов с атрибутами
    /// </summary>
    /// <returns></returns>
    [HttpGet("allInclude")]
    public async Task<ActionResult> GetAllInclude()
    {
        var user = GetUserIdByAuth();
        List<ObjTypeDto> list = new List<ObjTypeDto>();
        db.ObjTypes.Where(x => x.UserId == user.Id).ToList().ForEach(type =>
        {
            var typeDto = new ObjTypeDto
            {
                Id = type.Id,
                Name = type.Name,
                Code = type.Code,
                Description = type.Description,
                attributes = new List<ObjTypeAttributeDto>()
            };

            db.ObjTypeAttributes.Where(x => x.TypeId == type.Id && x.UserId == user.Id).ToList().ForEach(a =>
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

    /// <summary>
    /// Возвращает список всех типов без атрибутов
    /// </summary>
    /// <returns></returns>
    [HttpGet("all")]
    public async Task<ActionResult> GetAll()
    {
        var user = GetUserIdByAuth();

        List<ObjTypeDto> list = new List<ObjTypeDto>();
        db.ObjTypes.Where(x => x.UserId == user.Id).OrderBy(x => x.Id).ToList().ForEach(type =>
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


    /// <summary>
    /// Удаляет тип с заданным id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteItem(int id)
    {
        var user = GetUserIdByAuth();
        db.MenuElements.RemoveRange(db.MenuElements.Where(x => x.ObjTypeId == id && x.UserId == user.Id).ToList());

        db.ObjTypes.RemoveRange(db.ObjTypes.Where(x => x.Id == id && x.UserId == user.Id).ToArray());

        await db.SaveChangesAsync();

        return Ok();

    }


    /// <summary>
    /// Возвращает информацию о типе с атрибутами
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult> GetItem(int id)
    {
        var user = GetUserIdByAuth();
        var type = await db.ObjTypes.Include(x => x.Attributes)
            .AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.UserId == user.Id);

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

        if (type.Attributes != null)
        {
            type.Attributes.ToList().ForEach(a =>
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
        }

        return Ok(typeDto);

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
