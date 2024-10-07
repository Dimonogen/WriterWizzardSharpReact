using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiplomBackApi.Models
{
    /// <summary>
    /// Модель сущности пользователь
    /// </summary>
    [Table("user", Schema = "Diplom")]
    public class User
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("password")]
        public string Password { get; set; }
        [Column("description")]
        public string Description { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("role")]
        public string Role { get; set; }

        [Column("icon")]
        public string Icon { get; set; }

        [Column("lastauth")]
        public DateTime LastAuth { get; set; }

        [Column("project")]
        public string ProjectName { get; set; }
    }
}
