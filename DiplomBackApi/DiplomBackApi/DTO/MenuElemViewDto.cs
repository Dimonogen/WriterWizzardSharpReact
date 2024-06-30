using DiplomBackApi.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiplomBackApi.DTO
{
    public class MenuElemViewDto
    {
        public int id { get; set; }

        public string name { get; set; }

        public string? description { get; set; }

        public int? ParentMenuId { get; set; }
        
        public int? ObjTypeId { get; set; }
        
        public string Filters { get; set; }
    }
}
