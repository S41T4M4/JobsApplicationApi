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
        public string nome { get; set; }

        [Column("cnpj")]
        public string cnpj { get; set; }

    }
}
