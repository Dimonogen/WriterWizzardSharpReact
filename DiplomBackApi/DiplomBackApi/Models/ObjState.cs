using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace Litbase.Models
{
    /// <summary>
    /// Состояние объекта
    /// </summary>
    [Table("objstate", Schema = "Diplom"), PrimaryKey(nameof(Id), nameof(UserId))]
    public class ObjState : BaseEntity
    {
        /// <summary>
        /// Для того, чтобы не привязываться к id или другим полям, код для кода
        /// </summary>
        [Column("code")]
        public string Code { get; set; }

        /// <summary>
        /// Название состояния
        /// </summary>
        [DisallowNull]
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// Описание состояния
        /// </summary>
        [AllowNull]
        [Column("description")]
        public string? Description { get; set; }
    }
}
