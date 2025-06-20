using FitCompete.Domain.Entities;
using FitCompete.SharedKernel.Enums; // Potrzebne dla AchievementCondition
using Microsoft.EntityFrameworkCore;

namespace FitCompete.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Challenge> Challenges { get; set; }
        public DbSet<ChallengeAttempt> ChallengeAttempts { get; set; }
        public DbSet<ChallengeCategory> ChallengeCategories { get; set; }
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<UserAchievement> UserAchievements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Konfiguracja Relacji

            // Relacja User -> Challenge (CreatedChallenges)
            modelBuilder.Entity<User>()
                .HasMany(u => u.CreatedChallenges)
                .WithOne(c => c.CreatedByUser)
                .HasForeignKey(c => c.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacja User -> ChallengeAttempt
            modelBuilder.Entity<User>()
                .HasMany(u => u.ChallengeAttempts)
                .WithOne(ca => ca.User)
                .HasForeignKey(ca => ca.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacja Challenge -> ChallengeAttempt
            modelBuilder.Entity<Challenge>()
                .HasMany(c => c.ChallengeAttempts)
                .WithOne(ca => ca.Challenge)
                .HasForeignKey(ca => ca.ChallengeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacja ChallengeCategory -> Challenge
            modelBuilder.Entity<ChallengeCategory>()
                .HasMany(cc => cc.Challenges)
                .WithOne(c => c.ChallengeCategory)
                .HasForeignKey(c => c.ChallengeCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacja jeden-do-jednego: Challenge -> Achievement
            modelBuilder.Entity<Challenge>()
                .HasOne(c => c.Achievement)
                .WithOne(a => a.Challenge)
                .HasForeignKey<Achievement>(a => a.ChallengeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacja wiele-do-wielu: User <-> Achievement przez tabelę UserAchievement
            modelBuilder.Entity<UserAchievement>()
                .HasOne(ua => ua.User)
                .WithMany(u => u.UserAchievements)
                .HasForeignKey(ua => ua.UserId);

            modelBuilder.Entity<UserAchievement>()
                .HasOne(ua => ua.Achievement)
                .WithMany(a => a.UserAchievements)
                .HasForeignKey(ua => ua.AchievementId);

            #endregion

            #region Konfiguracja Indeksów

            // Dodajemy unikalne indeksy, aby zapobiec duplikatom w bazie danych.
            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<ChallengeCategory>()
                .HasIndex(c => c.Name)
                .IsUnique();

            #endregion

            // Wywołanie metody do wypełnienia bazy danych danymi testowymi
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            var seedDate = new DateTime(2024, 5, 21, 12, 0, 0, DateTimeKind.Utc);

            #region Kategorie

            var strengthCategory = new ChallengeCategory { ChallengeCategoryId = 1, Name = "Siła", Description = "Wyzwania oparte na sile mięśniowej." };
            var cardioCategory = new ChallengeCategory { ChallengeCategoryId = 2, Name = "Cardio", Description = "Wyzwania wytrzymałościowe." };
            var flexibilityCategory = new ChallengeCategory { ChallengeCategoryId = 3, Name = "Gimnastyka/Gibkość", Description = "Wyzwania związane z zakresem ruchu i kontrolą ciała." };
            modelBuilder.Entity<ChallengeCategory>().HasData(strengthCategory, cardioCategory, flexibilityCategory);

            #endregion

            #region Użytkownicy

            var adminUser = new User { UserId = 1, UserName = "Admin", Email = "admin@fitcompete.com", HashedPassword = "hashed_password_placeholder_admin", RegistrationDate = seedDate.AddDays(-10), IsAdmin = true };
            var user1 = new User { UserId = 2, UserName = "Rocky", Email = "rocky@stairway.com", HashedPassword = "hashed_password_placeholder_1", RegistrationDate = seedDate.AddDays(-5), IsAdmin = false };
            var user2 = new User { UserId = 3, UserName = "KasiaStrength", Email = "kasia@power.com", HashedPassword = "hashed_password_placeholder_2", RegistrationDate = seedDate.AddDays(-2), IsAdmin = false };
            modelBuilder.Entity<User>().HasData(adminUser, user1, user2);

            #endregion

            #region Wyzwania

            var challengePushups = new Challenge { ChallengeId = 1, Name = "100 Pompek na czas", Description = "Zrób 100 klasycznych pompek w jak najkrótszym czasie. Liczy się czas w sekundach.", UnitOfMeasure = "sekundy", CreatedDate = seedDate, CreatedByUserId = adminUser.UserId, ChallengeCategoryId = strengthCategory.ChallengeCategoryId };
            var challengeRun = new Challenge { ChallengeId = 2, Name = "Bieg na 5km", Description = "Przebiegnij 5 kilometrów w jak najkrótszym czasie. Wynik podaj w sekundach.", UnitOfMeasure = "sekundy", CreatedDate = seedDate, CreatedByUserId = adminUser.UserId, ChallengeCategoryId = cardioCategory.ChallengeCategoryId };
            var challengePlank = new Challenge { ChallengeId = 3, Name = "Plank (deska)", Description = "Utrzymaj pozycję deski jak najdłużej. Wynik to czas w sekundach.", UnitOfMeasure = "sekundy", CreatedDate = seedDate, CreatedByUserId = adminUser.UserId, ChallengeCategoryId = strengthCategory.ChallengeCategoryId };
            var challengePullups = new Challenge { ChallengeId = 4, Name = "Max podciągnięć", Description = "Wykonaj jak najwięcej podciągnięć nachwytem w jednej serii.", UnitOfMeasure = "powtórzenia", CreatedDate = seedDate, CreatedByUserId = adminUser.UserId, ChallengeCategoryId = strengthCategory.ChallengeCategoryId };
            modelBuilder.Entity<Challenge>().HasData(challengePushups, challengeRun, challengePlank, challengePullups);

            #endregion

            #region Osiągnięcia

            var achievementPushups = new Achievement { AchievementId = 1, ChallengeId = challengePushups.ChallengeId, Name = "Pompkowy Sprinter", Description = "Ukończ wyzwanie 100 pompek w mniej niż 180 sekund (3 minuty).", BadgeImageUrl = "/images/badges/pushup-master.png", Condition = AchievementCondition.LessThan, ThresholdValue = 180 };
            var achievementRun = new Achievement { AchievementId = 2, ChallengeId = challengeRun.ChallengeId, Name = "Maratończyk", Description = "Ukończ bieg na 5km w czasie poniżej 1500 sekund (25 minut).", BadgeImageUrl = "/images/badges/runner.png", Condition = AchievementCondition.LessThan, ThresholdValue = 1500 };
            var achievementPlank = new Achievement { AchievementId = 3, ChallengeId = challengePlank.ChallengeId, Name = "Żelazny Rdzeń", Description = "Utrzymaj pozycję deski przez ponad 120 sekund (2 minuty).", BadgeImageUrl = "/images/badges/plank-iron.png", Condition = AchievementCondition.GreaterThan, ThresholdValue = 120 };
            modelBuilder.Entity<Achievement>().HasData(achievementPushups, achievementRun, achievementPlank);

            #endregion

            #region Wyniki (ChallengeAttempts)

            // Wyniki dla wyzwania 100 pompek
            var attempt1_user1 = new ChallengeAttempt { ChallengeAttemptId = 1, UserId = user1.UserId, ChallengeId = challengePushups.ChallengeId, ResultValue = 240, AttemptDate = seedDate.AddDays(1) }; // Rocky - 4 minuty
            var attempt1_user2 = new ChallengeAttempt { ChallengeAttemptId = 2, UserId = user2.UserId, ChallengeId = challengePushups.ChallengeId, ResultValue = 175, AttemptDate = seedDate.AddDays(2) }; // Kasia - 2m 55s (zdobywa osiągnięcie!)

            // Wyniki dla biegu na 5km
            var attempt2_user1 = new ChallengeAttempt { ChallengeAttemptId = 3, UserId = user1.UserId, ChallengeId = challengeRun.ChallengeId, ResultValue = 1600, AttemptDate = seedDate.AddDays(3) }; // Rocky - 26m 40s

            // Wyniki dla deski
            var attempt3_user2 = new ChallengeAttempt { ChallengeAttemptId = 4, UserId = user2.UserId, ChallengeId = challengePlank.ChallengeId, ResultValue = 130, AttemptDate = seedDate.AddDays(4) }; // Kasia - 2m 10s (zdobywa osiągnięcie!)

            modelBuilder.Entity<ChallengeAttempt>().HasData(attempt1_user1, attempt1_user2, attempt2_user1, attempt3_user2);

            #endregion
        }
    }
}