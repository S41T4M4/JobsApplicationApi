using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobApplication.Domain.Models
{
    [Table("vagas")]
    public class Vagas
    {
        [Key]
        [Column("id")]
        public int id { get; set; }

        [Required]
        [Column("titulo")]
        [StringLength(255)]
        public string titulo { get; set; }

        [Required]
        [Column("descricao")]
        public string descricao { get; set; }

        [Required]
        [Column("requisitos")]
        public string requisitos { get; set; }

        [Required]
        [Column("salario")]
        [MaxLength(10)]
        public decimal salario { get; set; }

        [Required]
        [Column("localizacao")]
        [StringLength(255)]
        public string localizacao { get; set; }

        [Required]
        [Column("status")]
        [StringLength(50)]
        public string status { get; set; }

        [Required]
        [Column("id_recrutador")]
        public int id_recrutador { get; set; }

        [Column("data_criacao")]
        public DateTime data_criacao { get; set; } = DateTime.UtcNow;

        // Navegação para a tabela de usuarios
        [ForeignKey("id_recrutador")]
        public virtual Usuarios recrutador { get; set; }

    }
}
