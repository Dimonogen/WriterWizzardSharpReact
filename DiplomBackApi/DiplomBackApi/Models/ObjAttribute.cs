using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DiplomBackApi.Models
{
    /// <summary>
    /// Модель аттрибута объекта
    /// </summary>
    [Table("objattribute", Schema = "Diplom")]
    [Index(nameof(UserId)), PrimaryKey(nameof(Id), nameof(UserId))]
    public class ObjAttribute : BaseEntity
    {
        /// <summary>
        /// ID объекта
        /// </summary>
        public int ObjId { get; set; }
        [JsonIgnore, ForeignKey("ObjId, UserId")]
        public virtual Obj Obj { get; set; }

        /// <summary>
        /// Номер аттрибута
        /// </summary>
        [DisallowNull]
        [Column("number")]
        public int Number { get; set; }

        /// <summary>
        /// Значение аттрибута
        /// </summary>
        [DisallowNull]
        [Column("value")]
        public string Value { get; set; }
    }
}
