namespace DiplomBackApi.DTO;

public class SearchResultDto
{
    public ObjTypeDto Type { get; set; }

    public ObjDto Object { get; set; }


    public bool InName { get; set; }
    public List<SearchAttributeDto> InAttributes { get; set; }
}
