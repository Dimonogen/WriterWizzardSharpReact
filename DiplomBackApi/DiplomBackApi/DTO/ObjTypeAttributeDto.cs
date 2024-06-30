using DiplomBackApi.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DiplomBackApi.DTO
{
    public class ObjTypeAttributeDto
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }

        public int Type { get; set; }

        public string RegEx { get; set; }

        public bool IsComplex { get; set; }
    }
}
