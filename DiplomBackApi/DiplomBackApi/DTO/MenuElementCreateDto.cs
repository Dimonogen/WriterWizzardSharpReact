namespace DiplomBackApi.DTO
{
    public class MenuElementCreateDto
    {

        public string name { get; set; }

        public string? description { get; set; }

        public int RightId { get; set; }

        public int? ParentMenuId { get; set; }

        public int? ObjTypeId { get; set; }

        public string Filters { get; set; }
    }
}
