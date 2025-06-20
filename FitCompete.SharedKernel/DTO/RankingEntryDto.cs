namespace FitCompete.SharedKernel.Dtos
{
    public class RankingEntryDto
    {
        public string UserName { get; set; } = string.Empty;
        public decimal ResultValue { get; set; }
        public DateTime AttemptDate { get; set; }
    }
}