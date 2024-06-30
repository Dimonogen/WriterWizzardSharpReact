using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DiplomBackApi.Models
{
    /// <summary>
    /// Модель сущности объект, универсальная штука
    /// </summary>
    [Table("obj", Schema = "Diplom")]
    public class Obj
    {
        /// <summary>
        /// Идентификатор уникальный
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// Название объекта
        /// </summary>
        [DisallowNull]
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// Тип объекта
        /// </summary>
        [ForeignKey("ObjType")]
        public int TypeId {  get; set; }
        public ObjType ObjType { get; set; }

        /// <summary>
        /// Состояние объекта
        /// </summary>
        [Column("stateId", Order = 0), ForeignKey("ObjState")]
        public int StateId { get; set; }
        public ObjState State { get; set; }
    }
}
