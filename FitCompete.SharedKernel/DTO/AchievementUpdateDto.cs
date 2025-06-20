using System.ComponentModel.DataAnnotations;
using FitCompete.SharedKernel.Enums;

namespace FitCompete.SharedKernel.Dtos
{
    public class AchievementUpdateDto
    {
        [Required] public string Name { get; set; } = string.Empty;
        [Required] public string Description { get; set; } = string.Empty;
        [Required][Url] public string BadgeImageUrl { get; set; } = string.Empty;
        [Required] public AchievementCondition Condition { get; set; }
        [Required] public decimal ThresholdValue { get; set; }
        // Nie pozwalamy na zmianę powiązanego wyzwania podczas edycji
    }
}