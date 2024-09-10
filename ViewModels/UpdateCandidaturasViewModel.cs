using System.ComponentModel.DataAnnotations;

namespace JobApplication.ViewModels
{
    public class UpdateCandidaturaStatusViewModel
    {
        [Required]
        public string Status { get; set; } = string.Empty;

        public DateTime? DataCandidatura { get; set; }
    }
}
