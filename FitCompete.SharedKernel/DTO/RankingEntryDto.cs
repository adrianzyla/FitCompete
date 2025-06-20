﻿namespace FitCompete.SharedKernel.Dtos
{
    public class RankingEntryDto
    {
        public int AttemptId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public decimal ResultValue { get; set; }
        public DateTime AttemptDate { get; set; }
    }
}