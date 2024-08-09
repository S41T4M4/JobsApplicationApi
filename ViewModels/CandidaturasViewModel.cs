using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JobApplication.ViewModels
{
    public class CandidaturasViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("id_vaga")]
        public int IdVaga { get; set; }

        [JsonPropertyName("id_candidato")]
        public int IdCandidato { get; set; }

        [JsonPropertyName("nome_candidato")]
        public string NomeCandidato { get; set; }

        [JsonPropertyName("email_candidato")]
        public string EmailCandidato { get; set; }

        [JsonPropertyName("titulo_vagas")]
        public string TituloVaga { get; set; }

        [JsonPropertyName("id_recrutador")]
        public int IdRecrutador { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("data_candidatura")]
        public DateTime? DataCandidatura { get; set; }
    }
}
