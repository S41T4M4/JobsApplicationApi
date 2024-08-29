using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JobApplication.ViewModels
{
    public class EmpresasViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("nome")]
        public string Name { get; set; }
        [JsonPropertyName("cnpj")]
        public string Cnpj { get; set; }

    }
}
