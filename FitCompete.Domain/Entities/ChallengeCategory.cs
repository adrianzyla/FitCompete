using System.ComponentModel.DataAnnotations;

namespace FitCompete.Domain.Entities
{
    public class ChallengeCategory
    {
        public int ChallengeCategoryId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(250)]
        public string? Description { get; set; }

        // Relacje
        public virtual ICollection<Challenge> Challenges { get; set; } = new List<Challenge>();
    }
}