using System.Text.Json.Serialization;

namespace JobApplication.ViewModels
{
    public class VagasViewModel
    {
        [JsonPropertyName("titulo")]
        public string Titulo { get; set; }

        [JsonPropertyName("descricao")]
        public string Descricao { get; set; }

        [JsonPropertyName("requisitos")]
        public string Requisitos { get; set; }

        [JsonPropertyName("salario")]
        public decimal Salario { get; set; }

        [JsonPropertyName("localizacao")]
        public string Localizacao { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("id_recrutador")]
        public int IdRecrutador { get; set; }

        [JsonPropertyName("empresa_id")]
        public int IdEmpresa { get; set; }

        public string? Nome { get; set; }

    }
}
