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
    [Index(nameof(UserId))]
    public class ObjAttribute
    {
        /// <summary>
        /// Идентификатор уникальный
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// Id пользователя, чтобы для каждого юзера был "своя" БД
        /// </summary>
        [Column("userId")]
        public int UserId { get; set; }

        /// <summary>
        /// ID объекта
        /// </summary>
        [ForeignKey("Obj")]
        public int ObjId { get; set; }
        [JsonIgnore]
        public Obj Obj { get; set; }

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
