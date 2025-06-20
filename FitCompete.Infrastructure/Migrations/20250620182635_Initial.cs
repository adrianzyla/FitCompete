using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FitCompete.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChallengeCategories",
                columns: table => new
                {
                    ChallengeCategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChallengeCategories", x => x.ChallengeCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    HashedPassword = table.Column<string>(type: "TEXT", nullable: false),
                    ProfilePictureUrl = table.Column<string>(type: "TEXT", nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsAdmin = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Challenges",
                columns: table => new
                {
                    ChallengeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    UnitOfMeasure = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: false),
                    ChallengeCategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Challenges", x => x.ChallengeId);
                    table.ForeignKey(
                        name: "FK_Challenges_ChallengeCategories_ChallengeCategoryId",
                        column: x => x.ChallengeCategoryId,
                        principalTable: "ChallengeCategories",
                        principalColumn: "ChallengeCategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Challenges_Users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Achievements",
                columns: table => new
                {
                    AchievementId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    BadgeImageUrl = table.Column<string>(type: "TEXT", nullable: false),
                    Condition = table.Column<int>(type: "INTEGER", nullable: false),
                    ThresholdValue = table.Column<decimal>(type: "TEXT", nullable: false),
                    ChallengeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievements", x => x.AchievementId);
                    table.ForeignKey(
                        name: "FK_Achievements_Challenges_ChallengeId",
                        column: x => x.ChallengeId,
                        principalTable: "Challenges",
                        principalColumn: "ChallengeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChallengeAttempts",
                columns: table => new
                {
                    ChallengeAttemptId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ResultValue = table.Column<decimal>(type: "TEXT", nullable: false),
                    AttemptDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EvidenceUrl = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    ChallengeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChallengeAttempts", x => x.ChallengeAttemptId);
                    table.ForeignKey(
                        name: "FK_ChallengeAttempts_Challenges_ChallengeId",
                        column: x => x.ChallengeId,
                        principalTable: "Challenges",
                        principalColumn: "ChallengeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChallengeAttempts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAchievements",
                columns: table => new
                {
                    UserAchievementId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    AchievementId = table.Column<int>(type: "INTEGER", nullable: false),
                    DateEarned = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAchievements", x => x.UserAchievementId);
                    table.ForeignKey(
                        name: "FK_UserAchievements_Achievements_AchievementId",
                        column: x => x.AchievementId,
                        principalTable: "Achievements",
                        principalColumn: "AchievementId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAchievements_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ChallengeCategories",
                columns: new[] { "ChallengeCategoryId", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Wyzwania oparte na sile mięśniowej.", "Siła" },
                    { 2, "Wyzwania wytrzymałościowe.", "Cardio" },
                    { 3, "Wyzwania związane z zakresem ruchu i kontrolą ciała.", "Gimnastyka/Gibkość" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "HashedPassword", "IsAdmin", "ProfilePictureUrl", "RegistrationDate", "UserName" },
                values: new object[,]
                {
                    { 1, "admin@fitcompete.com", "hashed_password_placeholder_admin", true, null, new DateTime(2024, 5, 11, 12, 0, 0, 0, DateTimeKind.Utc), "Admin" },
                    { 2, "rocky@stairway.com", "hashed_password_placeholder_1", false, null, new DateTime(2024, 5, 16, 12, 0, 0, 0, DateTimeKind.Utc), "Rocky" },
                    { 3, "kasia@power.com", "hashed_password_placeholder_2", false, null, new DateTime(2024, 5, 19, 12, 0, 0, 0, DateTimeKind.Utc), "KasiaStrength" }
                });

            migrationBuilder.InsertData(
                table: "Challenges",
                columns: new[] { "ChallengeId", "ChallengeCategoryId", "CreatedByUserId", "CreatedDate", "Description", "Name", "UnitOfMeasure" },
                values: new object[,]
                {
                    { 1, 1, 1, new DateTime(2024, 5, 21, 12, 0, 0, 0, DateTimeKind.Utc), "Zrób 100 klasycznych pompek w jak najkrótszym czasie. Liczy się czas w sekundach.", "100 Pompek na czas", "sekundy" },
                    { 2, 2, 1, new DateTime(2024, 5, 21, 12, 0, 0, 0, DateTimeKind.Utc), "Przebiegnij 5 kilometrów w jak najkrótszym czasie. Wynik podaj w sekundach.", "Bieg na 5km", "sekundy" },
                    { 3, 1, 1, new DateTime(2024, 5, 21, 12, 0, 0, 0, DateTimeKind.Utc), "Utrzymaj pozycję deski jak najdłużej. Wynik to czas w sekundach.", "Plank (deska)", "sekundy" },
                    { 4, 1, 1, new DateTime(2024, 5, 21, 12, 0, 0, 0, DateTimeKind.Utc), "Wykonaj jak najwięcej podciągnięć nachwytem w jednej serii.", "Max podciągnięć", "powtórzenia" }
                });

            migrationBuilder.InsertData(
                table: "Achievements",
                columns: new[] { "AchievementId", "BadgeImageUrl", "ChallengeId", "Condition", "Description", "Name", "ThresholdValue" },
                values: new object[,]
                {
                    { 1, "/images/badges/pushup-master.png", 1, 0, "Ukończ wyzwanie 100 pompek w mniej niż 180 sekund (3 minuty).", "Pompkowy Sprinter", 180m },
                    { 2, "/images/badges/runner.png", 2, 0, "Ukończ bieg na 5km w czasie poniżej 1500 sekund (25 minut).", "Maratończyk", 1500m },
                    { 3, "/images/badges/plank-iron.png", 3, 1, "Utrzymaj pozycję deski przez ponad 120 sekund (2 minuty).", "Żelazny Rdzeń", 120m }
                });

            migrationBuilder.InsertData(
                table: "ChallengeAttempts",
                columns: new[] { "ChallengeAttemptId", "AttemptDate", "ChallengeId", "EvidenceUrl", "ResultValue", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 5, 22, 12, 0, 0, 0, DateTimeKind.Utc), 1, null, 240m, 2 },
                    { 2, new DateTime(2024, 5, 23, 12, 0, 0, 0, DateTimeKind.Utc), 1, null, 175m, 3 },
                    { 3, new DateTime(2024, 5, 24, 12, 0, 0, 0, DateTimeKind.Utc), 2, null, 1600m, 2 },
                    { 4, new DateTime(2024, 5, 25, 12, 0, 0, 0, DateTimeKind.Utc), 3, null, 130m, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_ChallengeId",
                table: "Achievements",
                column: "ChallengeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeAttempts_ChallengeId",
                table: "ChallengeAttempts",
                column: "ChallengeId");

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeAttempts_UserId",
                table: "ChallengeAttempts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeCategories_Name",
                table: "ChallengeCategories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Challenges_ChallengeCategoryId",
                table: "Challenges",
                column: "ChallengeCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Challenges_CreatedByUserId",
                table: "Challenges",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAchievements_AchievementId",
                table: "UserAchievements",
                column: "AchievementId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAchievements_UserId",
                table: "UserAchievements",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChallengeAttempts");

            migrationBuilder.DropTable(
                name: "UserAchievements");

            migrationBuilder.DropTable(
                name: "Achievements");

            migrationBuilder.DropTable(
                name: "Challenges");

            migrationBuilder.DropTable(
                name: "ChallengeCategories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
