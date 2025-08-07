namespace Litbase.DTO.Response;

/// <summary>
/// Класс для возврата ошибок
/// </summary>
public class ResponseDtoError
{
    /// <summary>
    /// Поле ошибки
    /// </summary>
    public string Error { get; set; }

    /// <summary>
    /// Поле возрата (на будущее)
    /// </summary>
    public object? Response { get; set; }

    /// <summary>
    /// Конструктор для ошибки
    /// </summary>
    /// <param name="error"></param>
    public ResponseDtoError(string error)
    {
        this.Error = error;
        this.Response = null;
    }
}
