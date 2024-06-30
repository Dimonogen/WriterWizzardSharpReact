using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DiplomBackApi.Models
{
    /// <summary>
    /// Модель сущности тип объекта
    /// </summary>
    [Table("objtype", Schema = "Diplom")]
    public class ObjType
    {
        /// <summary>
        /// Идентификатор уникальный
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }

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

    }
}
