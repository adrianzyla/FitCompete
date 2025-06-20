using System.ComponentModel.DataAnnotations;
using FitCompete.SharedKernel.Enums;

namespace FitCompete.Domain.Entities
{
   

    public class Achievement
    {
        public int AchievementId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string BadgeImageUrl { get; set; } = string.Empty;

        // NOWE POLA DEFINIUJĄCE WARUNEK
        [Required]
        public AchievementCondition Condition { get; set; }

        [Required]
        public decimal ThresholdValue { get; set; } // Wartość progowa (np. 120 dla sekund, 100 dla pompek)


        // Klucz obcy - bez zmian
        public int ChallengeId { get; set; }
        public virtual Challenge Challenge { get; set; } = null!;
        public virtual ICollection<UserAchievement> UserAchievements { get; set; } = new List<UserAchievement>();
    }
}