using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Panels.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SocialMediaLinks",
                schema: "panels",
                table: "Contractors",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SocialMediaLinks",
                schema: "panels",
                table: "Contractors");
        }
    }
}
