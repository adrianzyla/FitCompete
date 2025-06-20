using System.ComponentModel.DataAnnotations;

namespace FitCompete.SharedKernel.Dtos
{
    public class ChallengeUpdateDto
    {
        [Required(ErrorMessage = "Nazwa wyzwania jest wymagana.")]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Opis wyzwania jest wymagany.")]
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Jednostka miary jest wymagana.")]
        [MaxLength(20)]
        public string UnitOfMeasure { get; set; } = string.Empty;

        [Required(ErrorMessage = "Należy wybrać kategorię.")]
        [Range(1, int.MaxValue, ErrorMessage = "Nieprawidłowy ID kategorii.")]
        public int ChallengeCategoryId { get; set; }
    }
}