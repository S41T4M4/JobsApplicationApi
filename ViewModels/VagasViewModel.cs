using System.Text.Json.Serialization;

namespace JobApplication.ViewModels
{
    public class VagasViewModel
    {
        [JsonPropertyName("titulo")]
        public string Titulo { get; set; } = string.Empty;

        [JsonPropertyName("descricao")]
        public string Descricao { get; set; } = string.Empty;

        [JsonPropertyName("requisitos")]
        public string Requisitos { get; set; } = string.Empty;

        [JsonPropertyName("salario")]
        public decimal Salario { get; set; }

        [JsonPropertyName("localizacao")]
        public string Localizacao { get; set; } = string.Empty;

        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("id_recrutador")]
        public int IdRecrutador { get; set; }

        [JsonPropertyName("empresa_id")]
        public int IdEmpresa { get; set; }

        public string? Nome { get; set; } // Pode ser nulo se não for sempre necessário
    }
}
