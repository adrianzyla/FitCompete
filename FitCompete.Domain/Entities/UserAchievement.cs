using System.ComponentModel.DataAnnotations.Schema;

namespace FitCompete.Domain.Entities
{
    public class UserAchievement
    {
        public int UserAchievementId { get; set; }
        public int UserId { get; set; }
        public int AchievementId { get; set; }
        public DateTime DateEarned { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; } = null!;

        [ForeignKey("AchievementId")]
        public virtual Achievement Achievement { get; set; } = null!;
    }
}