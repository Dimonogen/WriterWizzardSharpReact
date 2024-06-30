using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DiplomBackApi.Models
{
    /// <summary>
    /// Модель аттрибута объекта
    /// </summary>
    [Table("objadditionalattribute", Schema = "Diplom")]
    public class ObjAdditionalAttribute
    {
        /// <summary>
        /// Идентификатор уникальный
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// ID объекта
        /// </summary>
        [ForeignKey("Obj")]
        public int ObjId { get; set; }
        public Obj Obj { get; set; }

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
        /// Значение аттрибута
        /// </summary>
        [DisallowNull]
        [Column("value")]
        public string Value { get; set; }

        /// <summary>
        /// Тип атрибута
        /// </summary>
        [DisallowNull]
        [ForeignKey("AttributeType")]
        public int AttributeTypeId { get; set; }
        public virtual AttributeType AttributeType { get; set; }

    }
}
