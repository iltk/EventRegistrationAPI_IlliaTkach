using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventRegistrationAPI_IlliaTkach.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Location = table.Column<string>(type: "TEXT", nullable: false),
                    EventDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Capacity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventId);
                });

            migrationBuilder.CreateTable(
                name: "Registrations",
                columns: table => new
                {
                    RegistrationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EventId = table.Column<int>(type: "INTEGER", nullable: false),
                    StudentName = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    RegisteredOn = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registrations", x => x.RegistrationId);
                    table.ForeignKey(
                        name: "FK_Registrations_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "Capacity", "EventDate", "Location", "Title" },
                values: new object[,]
                {
                    { 1, 100, new DateTime(2026, 9, 12, 10, 0, 0, 0, DateTimeKind.Unspecified), "Room E202", "Orientation" },
                    { 2, 30, new DateTime(2027, 6, 15, 14, 0, 0, 0, DateTimeKind.Unspecified), "AI Lab", "AI Workshop" },
                    { 3, 2, new DateTime(2027, 6, 20, 11, 0, 0, 0, DateTimeKind.Unspecified), "Room B200", "Career Mentorship Session" },
                    { 4, 50, new DateTime(2027, 7, 10, 13, 0, 0, 0, DateTimeKind.Unspecified), "Lecture Hall", "AI Seminar" }
                });

            migrationBuilder.InsertData(
                table: "Registrations",
                columns: new[] { "RegistrationId", "Email", "EventId", "RegisteredOn", "StudentName" },
                values: new object[,]
                {
                    { 1, "john@gmail.com", 3, new DateTime(2026, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "John Doe" },
                    { 2, "illia@gmail.com", 3, new DateTime(2026, 11, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Illia Tkach" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_EventId",
                table: "Registrations",
                column: "EventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Registrations");

            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
