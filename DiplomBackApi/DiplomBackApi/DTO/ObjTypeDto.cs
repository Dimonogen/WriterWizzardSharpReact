using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Litbase.DTO
{
    public class ObjTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code {  get; set; }
        public string? Description { get; set; }
        public ICollection<ObjTypeAttributeDto> attributes { get; set; }
    }
}
