using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ChessTournament.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Elo = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.Id);
                    table.CheckConstraint("CK_ELO", "ELO BETWEEN 0 AND 3000");
                });

            migrationBuilder.CreateTable(
                name: "Tournament",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Place = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlayerMin = table.Column<int>(type: "int", nullable: false),
                    PlayerMax = table.Column<int>(type: "int", nullable: false),
                    EloMin = table.Column<int>(type: "int", nullable: true),
                    EloMax = table.Column<int>(type: "int", nullable: true),
                    State = table.Column<int>(type: "int", nullable: false),
                    ActualRound = table.Column<int>(type: "int", nullable: false),
                    WomenOnly = table.Column<bool>(type: "bit", nullable: false),
                    RegistrationEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournament", x => x.Id);
                    table.CheckConstraint("CK_ELO_MAX", "ELOMAX IS NULL OR (ELOMAX BETWEEN 0 AND 3000)");
                    table.CheckConstraint("CK_ELO_MIN", "ELOMIN IS NULL OR (ELOMIN BETWEEN 0 AND 3000)");
                    table.CheckConstraint("CK_PLAYER_MAX", "PLAYERMAX BETWEEN 0 AND 32");
                    table.CheckConstraint("CK_PLAYER_MIN", "PLAYERMIN BETWEEN 0 AND 32");
                });

            migrationBuilder.CreateTable(
                name: "MM_Tournament_Category",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "int", nullable: false),
                    TournamentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MM_Tournament_Category", x => new { x.CategoriesId, x.TournamentsId });
                    table.ForeignKey(
                        name: "FK_MM_Tournament_Category_Category_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MM_Tournament_Category_Tournament_TournamentsId",
                        column: x => x.TournamentsId,
                        principalTable: "Tournament",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MM_Tournament_Player",
                columns: table => new
                {
                    MembersId = table.Column<int>(type: "int", nullable: false),
                    TournamentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MM_Tournament_Player", x => new { x.MembersId, x.TournamentsId });
                    table.ForeignKey(
                        name: "FK_MM_Tournament_Player_Member_MembersId",
                        column: x => x.MembersId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MM_Tournament_Player_Tournament_TournamentsId",
                        column: x => x.TournamentsId,
                        principalTable: "Tournament",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Junior" },
                    { 2, "Veteran" },
                    { 3, "Senior" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Member_Mail",
                table: "Member",
                column: "Mail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Member_Username",
                table: "Member",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MM_Tournament_Category_TournamentsId",
                table: "MM_Tournament_Category",
                column: "TournamentsId");

            migrationBuilder.CreateIndex(
                name: "IX_MM_Tournament_Player_TournamentsId",
                table: "MM_Tournament_Player",
                column: "TournamentsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MM_Tournament_Category");

            migrationBuilder.DropTable(
                name: "MM_Tournament_Player");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Member");

            migrationBuilder.DropTable(
                name: "Tournament");
        }
    }
}
