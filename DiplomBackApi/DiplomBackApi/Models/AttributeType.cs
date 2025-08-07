using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Litbase.Models
{
    /// <summary>
    /// Тип атрибута, примитив число, текст или объект, или даже один из перечня объектов
    /// </summary>
    [Table("attributetype", Schema = "Diplom")]
    [PrimaryKey(nameof(Id), nameof(UserId))]
    [Index(nameof(UserId))]
    public class AttributeType : BaseEntity
    {
        /// <summary>
        /// Название объекта
        /// </summary>
        [DisallowNull]
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// Примитив или ID объекта
        /// </summary>
        [DisallowNull]
        [Column("is_complex")]
        public bool IsComplex { get; set; }

        /// <summary>
        /// Само значение, какой примитив 0-3, какой Id
        /// </summary>
        [DisallowNull]
        [Column("type")]
        public int Type {  get; set; }

        /// <summary>
        /// Регулярное выражение для валидации значения поля на фронте
        /// </summary>
        [DisallowNull]
        [Column("validation")]
        public string RegExValidation { get; set; }
    }
}
