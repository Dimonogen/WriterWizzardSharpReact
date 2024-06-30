using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DiplomBackApi.Models
{
    /// <summary>
    /// Роль пользователей, описывающие доступ к функционалу системы
    /// </summary>
    [Table("role", Schema = "Diplom")]
    public class Role
    {
        /// <summary>
        /// Идентификатор уникальный
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// Название роли
        /// </summary>
        [DisallowNull]
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// Описание роли
        /// </summary>
        [AllowNull]
        [Column("description")]
        public string? Description { get; set; }
    }
}
