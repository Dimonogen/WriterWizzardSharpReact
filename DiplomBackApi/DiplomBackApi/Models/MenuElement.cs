using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DiplomBackApi.Models
{
    /// <summary>
    /// Пункт меню, к которому что-то привязано
    /// </summary>
    [Table("menuelement", Schema = "Diplom")]
    [Index(nameof(UserId))]
    public class MenuElement
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
        /// Отображаемое название пункта меню
        /// </summary>
        [Column("name"), DisallowNull]
        public string Name { get; set; }

        /// <summary>
        /// Описание пункта меню
        /// </summary>
        [Column("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Родительский пункт меню
        /// </summary>
        [AllowNull, ForeignKey("MenuElement"), Column("parentMenuId")]
        public int? ParentMenuId { get; set; }
        [JsonIgnore]
        public MenuElement? ParentMenu { get; set; }

        /// <summary>
        /// Тип объект, который отображается в гриде на пункте меню
        /// </summary>
        [ForeignKey("ObjType"), Column("objTypeId")]
        public int? ObjTypeId {  get; set; }
        [JsonIgnore]
        public ObjType? ObjType { get; set; }

        /// <summary>
        /// Фильтр для грида, типо по статусу какому-нибудь что-то скрыть и тп.
        /// </summary>
        [Column("filters")]
        public string Filters { get; set; }
    }
}
