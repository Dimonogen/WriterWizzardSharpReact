using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DiplomBackApi.Models
{
    /// <summary>
    /// Модель типа аттрибута объекта
    /// </summary>
    [Table("objtypeattribute", Schema = "Diplom")]
    [Index(nameof(UserId)), PrimaryKey(nameof(Id), nameof(UserId))]
    public class ObjTypeAttribute : BaseEntity
    {
        /// <summary>
        /// Тип объекта
        /// </summary>
        public int TypeId { get; set; }
        [JsonIgnore, ForeignKey("TypeId, UserId")]
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
        public int AttributeTypeId { get; set; }
        [JsonIgnore, ForeignKey("AttributeTypeId, UserId")]
        public virtual AttributeType AttributeType { get; set; }
    }
}
