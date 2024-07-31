namespace JobApplication.ViewModels
{
    public class VagasViewModel
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Requisitos { get; set; }
        public decimal Salario { get; set; }
        public string Localizacao { get; set; }
        public string Status { get; set; }
        public int IdRecrutador { get; set; }
        // A data_criacao não será definida pelo ViewModel
    }
}
