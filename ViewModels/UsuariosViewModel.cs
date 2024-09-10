using System.ComponentModel.DataAnnotations;

namespace JobApplication.ViewModels
{
    public class UsuariosViewModel
    {
        [Required]
        public string Nome { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Senha { get; set; } = string.Empty;

        [Required]
        public string Perfil { get; set; } = string.Empty;

        public int? EmpresaId { get; set; } // Pode ser nulo se não for sempre necessário
    }
}
