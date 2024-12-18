namespace DiplomBackApi.DTO;

public class SearchDto
{
    public List<SearchResultDto> Results { get; set; }

    public int All {  get; set; }

    public int Find {  get; set; }
}
