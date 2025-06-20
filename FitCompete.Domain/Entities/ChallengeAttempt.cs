using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitCompete.Domain.Entities
{
    public class ChallengeAttempt
    {
        public int ChallengeAttemptId { get; set; }

        public decimal ResultValue { get; set; } // np. 100 (dla pompek), 3600 (dla czasu w sekundach)

        public DateTime AttemptDate { get; set; }

        public string? EvidenceUrl { get; set; } // Link do zdjęcia/filmu

        // Klucze obce
        public int UserId { get; set; }
        public int ChallengeId { get; set; }

        // Właściwości nawigacyjne
        [ForeignKey("UserId")]
        public virtual User User { get; set; } = null!;

        [ForeignKey("ChallengeId")]
        public virtual Challenge Challenge { get; set; } = null!;
    }
}