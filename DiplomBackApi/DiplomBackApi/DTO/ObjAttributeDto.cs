using Litbase.Models;

namespace Litbase.DTO
{
    /// <summary>
    /// ДТО для аттрибутов объекта
    /// </summary>
    public class ObjAttributeDto
    {
        public int? Id { get; set; }

        public int Number { get; set; }

        public string Name { get; set; }

        public string? Value { get; set; }

        public int TypeId { get; set; }

        public int Type {  get; set; }

        public string RegEx { get; set; }

        public bool IsComplexType { get; set; }

        public bool IsAdditional { get; set; }
    }
}
