using FitCompete.SharedKernel.Enums;
namespace FitCompete.SharedKernel.Dtos
{
    public class AchievementDto
    {
        public int AchievementId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string BadgeImageUrl { get; set; } = string.Empty;
        public int ChallengeId { get; set; }
        public AchievementCondition Condition { get; set; } // Musi tu być
        public decimal ThresholdValue { get; set; }    // Musi tu być
    }
}