namespace FitCompete.SharedKernel.Dtos
{
    public class ChallengeDto
    {
        public int ChallengeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string UnitOfMeasure { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string CreatedByUserName { get; set; } = string.Empty;
    }
}