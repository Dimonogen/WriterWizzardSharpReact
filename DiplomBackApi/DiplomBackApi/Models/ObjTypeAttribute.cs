using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DiplomBackApi.Models
{
    /// <summary>
    /// Модель типа аттрибута объекта
    /// </summary>
    [Table("objtypeattribute", Schema = "Diplom")]
    public class ObjTypeAttribute
    {
        /// <summary>
        /// Идентификатор уникальный
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// Тип объекта
        /// </summary>
        [ForeignKey("ObjType")]
        public int TypeId { get; set; }
        public virtual ObjType ObjType { get; set; }

        /// <summary>
        /// Номер аттрибута
        /// </summary>
        [DisallowNull]
        [Column("number")]
        public int Number { get; set; }

        /// <summary>
        /// Название аттрибута
        /// </summary>
        [DisallowNull]
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// Тип атрибута
        /// </summary>
        [ForeignKey("AttributeType")]
        public int AttributeTypeId { get; set; }
        public virtual AttributeType AttributeType { get; set; }
    }
}
