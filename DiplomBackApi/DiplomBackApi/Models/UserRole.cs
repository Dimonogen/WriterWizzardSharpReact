using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DiplomBackApi.Models
{
    /// <summary>
    /// Связь пользователей и ролей
    /// </summary>
    [Table("userrole", Schema = "Diplom")]
    [PrimaryKey(nameof(UserId), nameof(RoleId))]
    public class UserRole
    {
        [Column("userId", Order = 0), ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        [Column("role", Order = 1), ForeignKey("Role")]
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
