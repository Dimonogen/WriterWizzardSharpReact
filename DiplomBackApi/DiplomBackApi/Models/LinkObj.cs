using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Litbase.Models
{
    /// <summary>
    /// Таблица связи объектов
    /// </summary>
    [Table("linkobj", Schema = "Diplom")]
    [PrimaryKey(nameof(ObjParentId), nameof(UserId))]
    [Index(nameof(UserId))]
    public class LinkObj
    {
        /// <summary>
        /// Объект к которому привязывается другой объект
        /// </summary>
        public int ObjParentId { get; set; }
        [JsonIgnore, ForeignKey($"{nameof(ObjParentId)}, {nameof(UserId)}")]
        public Obj ObjParent { get; set; }

        /// <summary>
        /// Объект, который привязываем
        /// </summary>
        public int ObjChildId { get; set; }
        [JsonIgnore, ForeignKey($"{nameof(ObjChildId)}, {nameof(UserId)}")]
        public Obj ObjChild { get; set; }


        /// <summary>
        /// Id пользователя, чтобы для каждого юзера был "своя" БД
        /// </summary>
        [Column("userId")]
        public int UserId { get; set; }
    }
}
