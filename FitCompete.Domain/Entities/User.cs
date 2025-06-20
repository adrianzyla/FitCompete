using System.ComponentModel.DataAnnotations;

namespace FitCompete.Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string HashedPassword { get; set; } = string.Empty;

        public string? ProfilePictureUrl { get; set; }

        public DateTime RegistrationDate { get; set; }

        public bool IsAdmin { get; set; } 

        public virtual ICollection<ChallengeAttempt> ChallengeAttempts { get; set; } = new List<ChallengeAttempt>();
        public virtual ICollection<Challenge> CreatedChallenges { get; set; } = new List<Challenge>();
        public virtual ICollection<UserAchievement> UserAchievements { get; set; } = new List<UserAchievement>(); // NOWA WŁAŚCIWOŚĆ
    }
}