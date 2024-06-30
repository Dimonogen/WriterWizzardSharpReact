namespace DiplomBackApi.DTO
{
    public class AttributeTypeDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsComplex { get; set; }

        public int Type {  get; set; }

        public string RegEx { get; set; }

        public AttributeTypeDto(Models.AttributeType type)
        {
            Id = type.Id;
            Name = type.Name;
            Type = type.Type;
            IsComplex = type.IsComplex;
            RegEx = type.RegExValidation;
        }
    }
}
