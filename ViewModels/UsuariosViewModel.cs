namespace JobApplication.ViewModels
{
    public class UsuariosViewModel
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Perfil { get; set; }
        public int? EmpresaId { get; set; }
        public string Cnpj { get; set; }
        public string NomeEmpresa { get; set; } // Nome da empresa para criar
    }

}
