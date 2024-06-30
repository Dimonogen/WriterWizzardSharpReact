using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiplomBackApi.Models
{
    /// <summary>
    /// Таблица избранных объектов
    /// </summary>
    [Table("favoriteobj", Schema = "Diplom")]
    [PrimaryKey(nameof(UserId), nameof(ObjId))]
    public class FavoriteObj
    {
        /// <summary>
        /// Пользователь
        /// </summary>
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        /// <summary>
        /// Объект добавленный в избранное
        /// </summary>
        [ForeignKey("Obj")]
        public int ObjId { get; set; }
        public Obj Obj { get; set; }
    }
}
