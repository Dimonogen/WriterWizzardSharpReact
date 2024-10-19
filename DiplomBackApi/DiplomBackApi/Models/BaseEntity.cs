using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiplomBackApi.Models
{
    /// <summary>
    /// Базовый объект с полем UserId
    /// </summary>
    [Index(nameof(UserId))]
    public class BaseEntity
    {
        /// <summary>
        /// Id пользователя, чтобы для каждого юзера был "своя" БД
        /// </summary>
        [Column("userId")]
        public int UserId { get; set; }
    }
}
