using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DiplomBackApi.Models
{
    /// <summary>
    /// Состояние объекта
    /// </summary>
    [Table("objstate", Schema = "Diplom")]
    public class ObjState
    {
        /// <summary>
        /// Идентификатор уникальный
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }

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
