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
    [Index(nameof(UserId)), PrimaryKey(nameof(Id), nameof(UserId))]
    public class Obj : BaseEntity
    {
        /// <summary> 
        /// Идентификатор уникальный в рамках пользователя
        /// </summary>
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
        public int TypeId {  get; set; }
        [JsonIgnore, ForeignKey("TypeId, UserId")]
        public virtual ObjType ObjType { get; set; }

        /// <summary>
        /// Состояние объекта
        /// </summary>
        [Column("stateId")]
        public int StateId { get; set; }
        [JsonIgnore, ForeignKey("StateId, UserId")]
        public virtual ObjState State { get; set; }
    }
}
