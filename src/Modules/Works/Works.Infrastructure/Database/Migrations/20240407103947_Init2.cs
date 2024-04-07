using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Works.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstimatedTimeInMinutes",
                schema: "works",
                table: "WorkItems");

            migrationBuilder.AddColumn<DateTime>(
                name: "EstimatedEndTime",
                schema: "works",
                table: "WorkItems",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EstimatedStartTime",
                schema: "works",
                table: "WorkItems",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstimatedEndTime",
                schema: "works",
                table: "WorkItems");

            migrationBuilder.DropColumn(
                name: "EstimatedStartTime",
                schema: "works",
                table: "WorkItems");

            migrationBuilder.AddColumn<int>(
                name: "EstimatedTimeInMinutes",
                schema: "works",
                table: "WorkItems",
                type: "integer",
                nullable: true);
        }
    }
}
