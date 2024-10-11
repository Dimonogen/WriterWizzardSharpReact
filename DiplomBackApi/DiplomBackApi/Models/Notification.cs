using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiplomBackApi.Models
{
    /// <summary>
    /// Уведомление для пользователя
    /// </summary>
    [Table("notification", Schema = "Diplom")]
    public class Notification
    {
        /// <summary>
        /// Идентификатор уникальный
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// Пользователь на кого уведомление
        /// </summary>
        [ForeignKey("User")]
        [Column("userId")]
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }

        /// <summary>
        /// Ссылка на объект, вызвавщий уведомление
        /// </summary>
        [ForeignKey("Obj")]
        [Column("objId")]
        public int ObjId { get; set; }
        [JsonIgnore]
        public Obj Obj { get; set; }

        /// <summary>
        /// Текст сообщения уведомления
        /// </summary>
        [Column("message")]
        public string Message { get; set; }

        /// <summary>
        /// Было ли прочитано данное уведомление
        /// </summary>
        [Column("isRead")]
        public bool IsRead { get; set; }
    }
}
