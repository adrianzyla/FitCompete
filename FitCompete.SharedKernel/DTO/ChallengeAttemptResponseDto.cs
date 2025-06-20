namespace FitCompete.SharedKernel.Dtos
{
    public class ChallengeAttemptResponseDto
    {
        public int ChallengeAttemptId { get; set; }
        public decimal ResultValue { get; set; }
        public DateTime AttemptDate { get; set; }
        public AchievementDto? EarnedAchievement { get; set; }
    }
}