using System.ComponentModel.DataAnnotations;

namespace FitCompete.SharedKernel.Dtos
{
    public class ChallengeAttemptRequestDto
    {
        [Required]
        public int ChallengeId { get; set; }

        [Required(ErrorMessage = "Podanie wyniku jest obowiązkowe.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Wynik musi być wartością dodatnią.")]
        public decimal? ResultValue { get; set; }

        [Url(ErrorMessage = "Podany link do dowodu jest nieprawidłowy.")]
        public string? EvidenceUrl { get; set; }

        [Required(ErrorMessage = "Imię jest wymagane.")]
        public string UserName { get; set; } = string.Empty;
    }
}