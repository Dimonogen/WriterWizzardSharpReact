using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DiplomBackApi.Models
{
    /// <summary>
    /// Связь пользователей и ролей
    /// </summary>
    [Table("rightrole", Schema = "Diplom")]
    [PrimaryKey(nameof(RightId), nameof(RoleId))]
    public class RightRole
    {
        [Column("rightId", Order = 0), ForeignKey("Right")]
        public int RightId { get; set; }
        public Right Right { get; set; }

        [Column("roleId", Order = 1), ForeignKey("Role")]
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
