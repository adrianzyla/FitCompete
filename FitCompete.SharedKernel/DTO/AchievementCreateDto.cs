using FitCompete.SharedKernel.Enums;
using System.ComponentModel.DataAnnotations;


namespace FitCompete.SharedKernel.Dtos
{
    public class AchievementCreateDto
    {
        [Required] public string Name { get; set; } = string.Empty;
        [Required] public string Description { get; set; } = string.Empty;
        [Required][Url] public string BadgeImageUrl { get; set; } = string.Empty;
        [Required] public AchievementCondition Condition { get; set; }
        [Required] public decimal ThresholdValue { get; set; }
        [Required] public int ChallengeId { get; set; }
    }
}