using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiplomBackApi.Models
{
    /// <summary>
    /// Таблица избранных объектов
    /// </summary>
    [Table("favoriteobj", Schema = "Diplom")]
    [PrimaryKey(nameof(ObjId), nameof(UserId))]
    [Index(nameof(UserId))]
    public class FavoriteObj
    {
        /// <summary>
        /// Пользователь
        /// </summary>
        [ForeignKey("User")]
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }

        /// <summary>
        /// Объект добавленный в избранное
        /// </summary>
        public int ObjId { get; set; }
        [JsonIgnore, ForeignKey($"{nameof(ObjId)}, {nameof(UserId)}")]
        public virtual Obj Obj { get; set; }
    }
}
