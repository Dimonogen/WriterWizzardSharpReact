using DiplomBackApi.Models;

namespace DiplomBackApi.DTO
{
    /// <summary>
    /// Это тот же самый объект, только объединённый с атрибутами
    /// </summary>
    public class ObjDto
    {
        public int Id { get; set; }

        public string State { get; set; }

        public int TypeId { get; set; }

        public string Name { get; set; }

        public ICollection<ObjAttributeDto> attributes { get; set; }


        public ObjDto(Obj obj)
        {
            Id = obj.Id;
            TypeId = obj.TypeId;
            Name = obj.Name;
            attributes = new List<ObjAttributeDto>();
        }
    }
}
