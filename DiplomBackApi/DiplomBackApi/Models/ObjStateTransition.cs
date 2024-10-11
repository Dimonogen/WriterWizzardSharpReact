using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DiplomBackApi.Models
{
    /// <summary>
    /// Переход из состояния в состояние объекта
    /// </summary>
    [Table("objstatetransition", Schema = "Diplom")]
    [PrimaryKey(nameof(StateFromId), nameof(StateToId))]
    [Index(nameof(UserId))]
    public class ObjStateTransiton
    {
        [Column("stateFromId", Order = 0), ForeignKey("ObjState")]
        public int StateFromId { get; set; }
        [JsonIgnore]
        public ObjState StateFrom { get; set; }

        [Column("stateToId", Order = 1), ForeignKey("ObjState")]
        public int StateToId { get; set; }
        [JsonIgnore]
        public ObjState StateTo { get; set; }

        /// <summary>
        /// Id пользователя, чтобы для каждого юзера был "своя" БД
        /// </summary>
        [Column("userId")]
        public int UserId { get; set; }
    }
}
