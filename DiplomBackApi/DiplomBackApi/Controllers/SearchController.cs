using Litbase.DTO;
using Litbase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
using System.Xml.Linq;

namespace Litbase.Controllers;

/// <summary>
/// Контроллер для поиска глобального
/// </summary>
[ApiController]
[Authorize]
[Route("api/search")]
public class SearchController : MyBaseController
{

    public SearchController(ApplicationContext context, IMemoryCache memoryCache) : base(context, memoryCache)
    {

    }


    /// <summary>
    /// Поиск текста по всем объектам
    /// </summary>
    /// <param name="text"></param>
    /// <returns>строка с настрйоками</returns>
    [HttpGet("")]
    public async Task<ActionResult> Search(string text)
    {
        var user = GetUserIdByAuth();

        var objFromNames = await db.Objs.Where(t => t.UserId == user.Id 
            && t.Name.ToLower().Contains(text.ToLower())).Include(t => t.State).Include(t => t.ObjType).ToListAsync();

        var objAttrs = await db.ObjAttributes.Where(x => x.UserId == user.Id 
            && x.Value.ToLower().Contains(text.ToLower()))
            .Include(t => t.Obj.State).Include(t=> t.Obj.ObjType).ToListAsync();

        var objExtAttrs = await db.ObjAdditionalAttributes.Where(x => x.UserId == user.Id 
            && x.Value.ToLower().Contains(text.ToLower())).Include(t => t.Obj.State).Include(t => t.Obj.ObjType).ToListAsync();

        var dic = new Dictionary<int, SearchResultDto>();

        foreach (var item in objFromNames)
        {
            dic.Add(item.Id, new SearchResultDto
            {
                Object = new ObjDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    State = item.State.Name,
                    TypeId = item.TypeId,
                    TypeName = item.ObjType.Name,
                },
                Type = new ObjTypeDto
                {
                    Id = item.ObjType.Id,
                    Name = item.ObjType.Name,
                    Code = item.ObjType.Code,
                    Description = item.ObjType.Description,
                },
                InName = true,
                InAttributes = new List<SearchAttributeDto>()
            });
        }

        var typesId = objAttrs.Select(a => a.Obj).Select(o => o.TypeId).Distinct().ToList();

        var attrTypes = await db.ObjTypeAttributes.Where(a => a.UserId == user.Id
            && typesId.Contains(a.TypeId)).ToListAsync();

        foreach (var item in objAttrs)
        {
            string name = attrTypes.FirstOrDefault(t => t.TypeId == item.Obj.TypeId).Name;
            if (dic.ContainsKey(item.ObjId))
            {
                dic[item.ObjId].InAttributes.Add(new SearchAttributeDto { 
                    Id = item.Id,
                    Number = item.Number,
                    Value = item.Value,
                    Name = name,
                    IsAdditional = false,
                });
            }
            else
            {
                dic.Add(item.ObjId,
                    new SearchResultDto
                    {
                        InName = false,
                        Object = new ObjDto
                        {
                            Id = item.Obj.Id,
                            Name = item.Obj.Name,
                            State = item.Obj.State.Name,
                        },
                        Type = new ObjTypeDto
                        {
                            Id = item.Obj.ObjType.Id,
                            Name = item.Obj.ObjType.Name,
                            Code = item.Obj.ObjType.Code,
                            Description = item.Obj.ObjType.Description,
                        },
                        InAttributes = new List<SearchAttributeDto>
                        {
                            new SearchAttributeDto {
                                IsAdditional = false,
                                Id = item.Id,
                                Value = item.Value,
                                Number = item.Number,
                                Name = name,
                            }
                        }
                    });
            }
        }

        foreach (var item in objExtAttrs)
        {
            if (dic.ContainsKey(item.ObjId))
            {
                dic[item.ObjId].InAttributes.Add(new SearchAttributeDto
                {
                    Id = item.Id,
                    Number = item.Number,
                    Value = item.Value,
                    Name = item.Name,
                    IsAdditional = true,
                });
            }
            else
            {
                dic.Add(item.ObjId,
                    new SearchResultDto
                    {
                        InName = false,
                        Object = new ObjDto
                        {
                            Id = item.Obj.Id,
                            Name = item.Obj.Name,
                            State = item.Obj.State.Name,
                        },
                        Type = new ObjTypeDto
                        {
                            Id = item.Obj.ObjType.Id,
                            Name = item.Obj.ObjType.Name,
                            Code = item.Obj.ObjType.Code,
                            Description = item.Obj.ObjType.Description,
                        },
                        InAttributes = new List<SearchAttributeDto>
                        {
                            new SearchAttributeDto {
                                IsAdditional = false,
                                Id = item.Id,
                                Value = item.Value,
                                Number = item.Number,
                                Name = item.Name,
                            }
                        }
                    });
            }
        }

        int allCount = await db.Objs.Where(t => t.UserId == user.Id).CountAsync();

        return Ok( new SearchDto { 
            Results = dic.Values.ToList(),
            All = allCount,
            Find = dic.Count,
        });
    }
}
