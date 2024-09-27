using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace DiplomBackApi.Models
{
    /// <summary>
    /// Состояние объекта
    /// </summary>
    [Table("objstate", Schema = "Diplom")]
    [Index(nameof(UserId))]
    public class ObjState
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
