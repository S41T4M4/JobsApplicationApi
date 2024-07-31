namespace JobApplication.ViewModels
{
    public class CandidaturasViewModel
    {
        public int Id { get; set; }

        public int IdVaga { get; set; }

        public int IdCandidato { get; set; }

        public string Status { get; set; }

        // A data de candidatura pode ser opcional no ViewModel para criação
        public DateTime? DataCandidatura { get; set; }
    }
}
