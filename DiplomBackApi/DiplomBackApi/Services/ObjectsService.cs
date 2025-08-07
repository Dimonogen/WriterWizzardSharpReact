using Litbase;
using Litbase.DTO;
using Litbase.Models;
using Microsoft.EntityFrameworkCore;

namespace Litbase.Services;

/// <summary>
/// Сервис для доступа к объектам
/// </summary>
public class ObjectsService
{
    /// <summary>
    /// БД
    /// </summary>
    protected ApplicationContext db { get; set; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="DB"></param>
    public ObjectsService(ApplicationContext DB)
    {
        db = DB;
    }


    /// <summary>
    /// Функция возвращает объект по его ID, обогащая сущность атрибутами
    /// </summary>
    /// <param name="id">айди объекта</param>
    /// <param name="userId">пользователь</param>
    /// <returns></returns>
    public async Task<ObjDto?> GetObjDtoAsync(int id, int? userId = null)
    {
        Obj? obj;

        if (userId == null)
            return null;//obj = await Objs.FirstOrDefaultAsync(x => x.Id == id);
        else
            obj = await db.Objs.Include(x => x.State).Include(x => x.Attributes)
                .Include(x => x.AdditionalAttributes).Include(x => x.ObjType.Attributes).AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);

        if (obj == null) { return null; }

        var attribTypes = await db.AttributeTypes.Where(x => x.UserId == userId).AsNoTracking().ToListAsync();

        ObjDto objDto = GetObjDtoFromData(obj, obj.ObjType, attribTypes);

        return objDto;

    }

    /// <summary>
    /// Получение списка объектов с атрибутами по типу
    /// </summary>
    /// <param name="typeId"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<List<ObjDto>> GetListObjDtoAsync(int typeId, int userId)
    {
        var stateDelete = await db.ObjStates.FirstOrDefaultAsync(x => x.Code == "deleted" && x.UserId == userId);

        if (stateDelete == null)
        {
            throw new Exception("Not found deleted state in DB");
        }

        var objs = await db.Objs.Include(x => x.State).Include(x => x.Attributes)
                .Include(x => x.AdditionalAttributes)
                .Where(x => x.TypeId == typeId && x.UserId == userId && x.StateId != stateDelete.Id)
                .AsNoTracking().ToListAsync();

        var type = await db.ObjTypes.Include(x => x.Attributes)
            .AsNoTracking().FirstOrDefaultAsync(x => x.Id == typeId && x.UserId == userId);

        var attribTypes = await db.AttributeTypes.Where(x => x.UserId == userId).AsNoTracking().ToListAsync();

        List<ObjDto> list = new List<ObjDto>();

        foreach (var obj in objs)
        {
            list.Add(
                GetObjDtoFromData(obj, type, attribTypes)
            );
            foreach (var attr in list.Last().attributes)
            {
                if (attr.IsComplexType && attr.Value != null)
                {
                    int id;
                    try { id = int.Parse(attr.Value); }
                    catch (Exception e) { continue; }
                    attr.Value = db.Objs.First(x => x.Id == id && x.UserId == userId).Name;
                }
            }
        }

        return list;
    }


    private ObjDto GetObjDtoFromData(Obj obj, ObjType objType, List<AttributeType> attribTypes)
    {
        ObjDto objDto = new ObjDto(obj);
        objDto.State = obj.State.Name ?? "Состояние";

        foreach (var tAttribute in objType.Attributes)
        {
            var attribute = obj.Attributes!.FirstOrDefault(x => x.Number == tAttribute.Number);
            var attrib_type = attribTypes.FirstOrDefault(x => x.Id == tAttribute.AttributeTypeId);

            if (attribute != null)
            {
                objDto.attributes.Add(new ObjAttributeDto
                {
                    Id = attribute.Id,
                    Name = tAttribute.Name,
                    Number = tAttribute.Number,
                    Value = attribute.Value,
                    TypeId = tAttribute.AttributeTypeId,
                    Type = attrib_type.Type,
                    RegEx = attrib_type.RegExValidation,
                    IsComplexType = attrib_type.IsComplex,
                    IsAdditional = false
                });
            }
            else
            {
                objDto.attributes.Add(new ObjAttributeDto
                {
                    Id = null,
                    Name = tAttribute.Name,
                    Number = tAttribute.Number,
                    Value = null,
                    TypeId = tAttribute.AttributeTypeId,
                    Type = attrib_type.Type,
                    RegEx = attrib_type.RegExValidation,
                    IsComplexType = attrib_type.IsComplex,
                    IsAdditional = false
                });
            }
        }

        foreach (var attr in obj.AdditionalAttributes!)
        {
            var attrType = attribTypes.FirstOrDefault(x => x.Id == attr.AttributeTypeId);
            objDto.extAttributes.Add(new ObjAttributeDto
            {
                Name = attr.Name,
                Number = attr.Number,
                Id = attr.Id,
                Value = attr.Value,
                Type = attrType.Type,
                IsComplexType = attrType.IsComplex,
                RegEx = attrType.RegExValidation,
                TypeId = attr.AttributeTypeId,
                IsAdditional = true,
            }
            );
        }

        return objDto;
    }
}
