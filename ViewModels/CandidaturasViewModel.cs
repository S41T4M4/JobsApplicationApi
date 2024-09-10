using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JobApplication.ViewModels
{
    public class CandidaturasViewModel
    {
        [Required]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Required]
        [JsonPropertyName("id_vaga")]
        public int IdVaga { get; set; }

        [Required]
        [JsonPropertyName("id_candidato")]
        public int IdCandidato { get; set; }

        [Required]
        [JsonPropertyName("nome_candidato")]
        public string NomeCandidato { get; set; } = string.Empty;

        [Required]
        [JsonPropertyName("email_candidato")]
        public string EmailCandidato { get; set; } = string.Empty;

        [Required]
        [JsonPropertyName("titulo_vagas")]
        public string TituloVaga { get; set; } = string.Empty;

        [Required]
        [JsonPropertyName("id_recrutador")]
        public int IdRecrutador { get; set; }

        [Required]
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        public DateTime? DataCandidatura { get; set; }
    }
}
