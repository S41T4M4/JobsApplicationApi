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
        public required string titulo { get; set; }

        [Required]
        [Column("descricao")]
        public required string descricao { get; set; }

        [Required]
        [Column("requisitos")]
        public required string requisitos { get; set; }

        [Required]
        [Column("salario")]
        [MaxLength(10)]
        public decimal salario { get; set; }

        [Required]
        [Column("localizacao")]
        [StringLength(255)]
        public required string localizacao { get; set; }

        [Required]
        [Column("status")]
        [StringLength(50)]
        public required string status { get; set; }

        [Required]
        [Column("id_recrutador")]
        public int id_recrutador { get; set; }

        [Column("data_criacao")]
        public DateTime data_criacao { get; set; } = DateTime.UtcNow;

        [ForeignKey("id_recrutador")]
        public virtual Usuarios recrutador { get; set; }

        [ForeignKey("Empresa")]
        [Column("empresa_id")]
        public int empresa_id { get; set; }

        public virtual Empresas Empresa { get; set; }
    }
}
