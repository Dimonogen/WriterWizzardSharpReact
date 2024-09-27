using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiplomBackApi.Models
{
    /// <summary>
    /// Таблица связи объектов
    /// </summary>
    [Table("linkobj", Schema = "Diplom")]
    [PrimaryKey(nameof(ObjParentId), nameof(ObjChildId))]
    [Index(nameof(UserId))]
    public class LinkObj
    {
        /// <summary>
        /// Объект к которому привязывается другой объект
        /// </summary>
        [ForeignKey("Obj")]
        public int ObjParentId { get; set; }
        public Obj ObjParent { get; set; }

        /// <summary>
        /// Объект, который привязываем
        /// </summary>
        [ForeignKey("Obj")]
        public int ObjChildId { get; set; }
        public Obj ObjChild { get; set; }


        /// <summary>
        /// Id пользователя, чтобы для каждого юзера был "своя" БД
        /// </summary>
        [Column("userId")]
        public int UserId { get; set; }
    }
}
