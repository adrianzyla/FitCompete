namespace FitCompete.SharedKernel.Dtos
{
    public class ChallengeCategoryDto
    {
        public int ChallengeCategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}