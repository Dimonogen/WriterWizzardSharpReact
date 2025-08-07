using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Litbase.Models
{
    /// <summary>
    /// Пункт меню, к которому что-то привязано
    /// </summary>
    [Table("menuelement", Schema = "Diplom")]
    [Index(nameof(UserId)), PrimaryKey(nameof(Id), nameof(UserId))]
    public class MenuElement : BaseEntity
    {
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
        [AllowNull, Column("parentMenuId")]
        public int? ParentMenuId { get; set; }
        [JsonIgnore, ForeignKey($"{nameof(ParentMenuId)}, {nameof(UserId)}")]
        public MenuElement? ParentMenu { get; set; }

        /// <summary>
        /// Тип объект, который отображается в гриде на пункте меню
        /// </summary>
        [Column("objTypeId")]
        public int? ObjTypeId {  get; set; }
        [JsonIgnore, ForeignKey("ObjTypeId, UserId")]
        public ObjType? ObjType { get; set; }

        /// <summary>
        /// Фильтр для грида, типо по статусу какому-нибудь что-то скрыть и тп.
        /// </summary>
        [Column("filters")]
        public string Filters { get; set; }
    }
}
