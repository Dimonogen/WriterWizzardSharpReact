using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DiplomBackApi.Models
{
    /// <summary>
    /// Модель сущности объект, универсальная штука
    /// </summary>
    [Table("obj", Schema = "Diplom")]
    [Index(nameof(UserId))]
    public class Obj
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
        [JsonIgnore]
        public ObjType ObjType { get; set; }

        /// <summary>
        /// Состояние объекта
        /// </summary>
        [Column("stateId", Order = 0), ForeignKey("ObjState")]
        public int StateId { get; set; }
        [JsonIgnore]
        public ObjState State { get; set; }
    }
}
