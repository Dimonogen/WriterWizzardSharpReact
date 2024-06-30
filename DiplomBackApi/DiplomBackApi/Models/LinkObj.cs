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
    }
}
