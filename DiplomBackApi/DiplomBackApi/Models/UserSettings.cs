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
    [Table("userSettings", Schema = "Diplom")]
    [PrimaryKey(nameof(UserId), nameof(Code))]
    [Index(nameof(UserId), nameof(Code))]
    public class UserSettings
    {
        /// <summary>
        /// Id пользователя, чтобы для каждого юзера был "своя" БД
        /// </summary>
        [Column("userId")]
        public int UserId { get; set; }

        /// <summary>
        /// Код грида
        /// </summary>
        [DisallowNull]
        [Column("code")]
        public string Code { get; set; }

        /// <summary>
        /// ТЗначение настроек
        /// </summary>
        [ForeignKey("value")]
        public string Value {  get; set; }
    }
}
