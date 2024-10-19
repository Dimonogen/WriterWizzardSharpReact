using DiplomBackApi.DTO;
using Microsoft.AspNetCore.Mvc;

namespace DiplomBackApi.Controllers;

/// <summary>
/// Контроллер api для объектов
/// </summary>
[ApiController]
[Route("api/attrType")]
public class AttributeTypeController : MyBaseController
{

    public AttributeTypeController(ApplicationContext context) : base(context)
    {
    }

    /// <summary>
    /// Создание типа
    /// </summary>
    /// <returns></returns>
    [HttpPost("create")]
    public async Task<ActionResult> Create(CreateAttrTypeModel typeModel)
    {
        var user = GetUserIdByAuth();   
        var type = new Models.AttributeType
        {
            Name = typeModel.name,
            Type = typeModel.type,
            IsComplex = typeModel.isComplex,
            RegExValidation = typeModel.regEx,
            UserId = user.Id,
        };

        await db.AttributeTypes.AddAsync(
            type
            );

        await db.SaveChangesAsync();

        AttributeTypeDto typeDto = new AttributeTypeDto(type);

        return Ok(typeDto);
        
    }

    /// <summary>
    /// Возвращает один тип
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult> GetItem(int id)
    {
        var user = GetUserIdByAuth();
        var type = db.AttributeTypes.FirstOrDefault(x=>x.Id== id && x.UserId == user.Id);

        if (type == null)
            return BadRequest();

        var typeDto = new AttributeTypeDto(type);

        return Ok(typeDto);
        
    }


    
    [HttpPost("edit")]
    public async Task<ActionResult> EditItem(EditAttrTypeModel typeModel)
    {
        var user = GetUserIdByAuth();
        var type_e = db.AttributeTypes.FirstOrDefault(x=>x.Id == typeModel.id && x.UserId == user.Id);

        if (type_e == null)
            return BadRequest();

        type_e.Name = typeModel.name;
        type_e.IsComplex = typeModel.is_complex;
        type_e.RegExValidation = typeModel.reg_ex;
        type_e.Type = typeModel.type;

        await db.SaveChangesAsync();

        return Ok();
        
    }

    /// <summary>
    /// Возвращает список всех типов атрибутов
    /// </summary>
    /// <returns></returns>
    [HttpGet("all")]
    public async Task<ActionResult> GetAll()
    {
        var user = GetUserIdByAuth();
        var list = new List<AttributeTypeDto>();
        db.AttributeTypes.Where(x => x.UserId == user.Id).OrderBy(x => x.Id).ToList().ForEach(t =>
            list.Add(new AttributeTypeDto(t))
        ); 

        return Ok(list);
        
    }


    /// <summary>
    /// Удаление типа
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteItem(int id)
    {
        var user = GetUserIdByAuth();
        db.AttributeTypes.RemoveRange(db.AttributeTypes.Where(x => x.Id == id && x.UserId == user.Id).ToArray());

        await db.SaveChangesAsync();

        return Ok();
        
    }
}


public class CreateAttrTypeModel
{
    public string name { get; set; }

    public bool isComplex { get; set; }

    public int type { get; set; }

    public string regEx { get; set; }
}

public class EditAttrTypeModel
{
    public int id { get; set; }
    public string name { get; set; }
    public bool is_complex { get; set; }

    public int type { get; set; }

    public string reg_ex { get; set; }
}
