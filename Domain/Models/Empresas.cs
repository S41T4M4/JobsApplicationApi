using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobApplication.Domain.Models
{
    [Table("empresas")]
    public class Empresas
    {
        [Key]
        [Column("id")]
        public int id { get; set; }
        [Column("nome")]
        public required string nome { get; set; }

        [Column("cnpj")]
        public required string cnpj { get; set; }

    }
}
