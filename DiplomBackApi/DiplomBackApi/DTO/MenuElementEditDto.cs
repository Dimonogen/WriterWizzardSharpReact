namespace Litbase.DTO;

/// <summary>
/// Дто для создания элемента меню
/// </summary>
public class MenuElementEditDto
{
    /// <summary>
    /// ID элемента меню
    /// </summary>
    public int id { get; set; }

    /// <summary>
    /// Имя или текст элемента меню
    /// </summary>
    public required string name { get; set; }
}
