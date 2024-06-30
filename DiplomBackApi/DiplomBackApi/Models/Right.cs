using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DiplomBackApi.Models
{
    /// <summary>
    /// Право, из нескольких прав состоит роль, 
    /// право даёт доступ к мелкому атомарному функционалу
    /// </summary>
    [Table("right", Schema = "Diplom"), Index(nameof(Code))]
    public class Right
    {
        /// <summary>
        /// Идентификатор уникальный
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// Код, по которому тоже можно искать права
        /// </summary>
        [DisallowNull]
        [Column("code")]
        public string Code { get; set; }

        /// <summary>
        /// Название права
        /// </summary>
        [DisallowNull]
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// Описание права
        /// </summary>
        [AllowNull]
        [Column("description")]
        public string? Description { get; set; }
    }
}
