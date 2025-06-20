using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitCompete.Domain.Entities
{
    public class Challenge
    {
        public int ChallengeId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string UnitOfMeasure { get; set; } = string.Empty; // np. "reps", "seconds", "meters", "kg"

        public DateTime CreatedDate { get; set; }

        // Klucze obce
        public int CreatedByUserId { get; set; }
        public int ChallengeCategoryId { get; set; }

        // Właściwości nawigacyjne
        [ForeignKey("CreatedByUserId")]
        public virtual User CreatedByUser { get; set; } = null!;

        [ForeignKey("ChallengeCategoryId")]
        public virtual ChallengeCategory ChallengeCategory { get; set; } = null!;
        public virtual Achievement? Achievement { get; set; }
        public virtual ICollection<ChallengeAttempt> ChallengeAttempts { get; set; } = new List<ChallengeAttempt>();
    }
}