using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Works.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "Priority",
                schema: "works",
                table: "GardeningWorks",
                type: "SMALLINT",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<string>(
                name: "Tags",
                schema: "works",
                table: "GardeningWorks",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priority",
                schema: "works",
                table: "GardeningWorks");

            migrationBuilder.DropColumn(
                name: "Tags",
                schema: "works",
                table: "GardeningWorks");
        }
    }
}
