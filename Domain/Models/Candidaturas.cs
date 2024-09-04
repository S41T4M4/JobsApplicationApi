using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobApplication.Domain.Models
{
    [Table("candidaturas")]
    public class Candidaturas
    {
        [Key]
        [Column("id")]
        public int id { get; set; }

        [Required]
        [Column("id_vaga")]
        public int id_vaga { get; set; }

        [Required]
        [Column("id_candidato")]
        public int id_candidato { get; set; }

        [Required]
        [Column("status")]
        [StringLength(50)]
        public string status { get; set; }

        [Column("data_candidatura")]
        public DateTime data_candidatura { get; set; } = DateTime.Now;

        // Navegação para as tabelas de vagas e candidatos 
        [ForeignKey("id_vaga")]
        public virtual Vagas vaga { get; set; }

        [ForeignKey("id_candidato")]
        public virtual Usuarios candidato { get; set; }


       
    }
}
