using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Works.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "works");

            migrationBuilder.CreateTable(
                name: "GardeningWorks",
                schema: "works",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BusinessId = table.Column<string>(type: "text", nullable: false),
                    ClientEmail = table.Column<string>(type: "text", nullable: true),
                    PlannedStartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RealStartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PlannedEndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RealEndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    City = table.Column<string>(type: "text", nullable: false),
                    Street = table.Column<string>(type: "text", nullable: false),
                    NumberStreet = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<short>(type: "SMALLINT", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GardeningWorks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkItems",
                schema: "works",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BusinessId = table.Column<string>(type: "text", nullable: false),
                    GardeningWorkId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    EstimatedTimeInMinutes = table.Column<int>(type: "integer", nullable: true),
                    RealTimeInMinutes = table.Column<int>(type: "integer", nullable: true),
                    Status = table.Column<short>(type: "SMALLINT", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkItems_GardeningWorks_GardeningWorkId",
                        column: x => x.GardeningWorkId,
                        principalSchema: "works",
                        principalTable: "GardeningWorks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeWeatherRecords",
                schema: "works",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TimeLogMinutes = table.Column<short>(type: "smallint", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TimeLogCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    WorkItemId = table.Column<int>(type: "integer", nullable: false),
                    Weathers = table.Column<string>(type: "text", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeWeatherRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeWeatherRecords_WorkItems_WorkItemId",
                        column: x => x.WorkItemId,
                        principalSchema: "works",
                        principalTable: "WorkItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TimeWeatherRecords_WorkItemId",
                schema: "works",
                table: "TimeWeatherRecords",
                column: "WorkItemId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkItems_GardeningWorkId",
                schema: "works",
                table: "WorkItems",
                column: "GardeningWorkId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeWeatherRecords",
                schema: "works");

            migrationBuilder.DropTable(
                name: "WorkItems",
                schema: "works");

            migrationBuilder.DropTable(
                name: "GardeningWorks",
                schema: "works");
        }
    }
}
