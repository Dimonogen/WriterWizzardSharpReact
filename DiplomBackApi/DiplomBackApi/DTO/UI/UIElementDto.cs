using DiplomBackApi.Models.UI;

namespace DiplomBackApi.DTO.UI;

public class UIElementDto
{
    int PositionX { get; set; }
    int PositionY { get; set; }

    int SizeX { get; set; }
    int SizeY { get; set; }

    UIElementType ElementType { get; set; }
}
