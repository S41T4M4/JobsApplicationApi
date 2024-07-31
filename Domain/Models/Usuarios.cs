using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobApplication.Domain.Models
{
    [Table("usuarios")]
    public class Usuarios
    {
        [Key]
        [Column("id")]
        public int id { get; set; }

        [Required]
        [Column("nome")]
        [StringLength(100)]
        public string nome { get; set; }

        [Required]
        [Column("email")]
        [StringLength(100)]
        public string email { get; set; }

        [Required]
        [Column("senha")]
        [StringLength(255)]
        public string senha { get; set; }

        [Required]
        [Column("perfil")]
        [StringLength(50)]
        public string perfil { get; set; }

        [Column("data_criacao")]
        public DateTime data_criacao { get; set; } = DateTime.UtcNow;
    }
}
