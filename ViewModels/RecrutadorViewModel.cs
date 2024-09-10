using System.ComponentModel.DataAnnotations;

namespace JobApplication.ViewModels
{
    public class RecrutadorViewModel
    {
        [Required]
        public string Nome { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Senha { get; set; } = string.Empty;

        [Required]
        public string Cnpj { get; set; } = string.Empty;
    }
}
