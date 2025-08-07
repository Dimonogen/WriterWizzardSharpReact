using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Litbase.Models
{
    /// <summary>
    /// Модель сущности тип объекта
    /// </summary>
    [Table("objtype", Schema = "Diplom")]
    [PrimaryKey(nameof(Id), nameof(UserId))]
    [Index(nameof(UserId))]
    public class ObjType : BaseEntity
    {
        /// <summary>
        /// Название типа
        /// </summary>
        [DisallowNull]
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// Код типа
        /// </summary>
        [DisallowNull]
        [Column("code")]
        public string Code { get; set; }

        /// <summary>
        /// Описание типа
        /// </summary>
        [AllowNull]
        [Column("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Аттрибуты этого типа
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<ObjTypeAttribute>? Attributes { get; set; }
    }
}
