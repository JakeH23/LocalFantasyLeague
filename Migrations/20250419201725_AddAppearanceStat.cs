using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalFantasyLeague.Migrations
{
    /// <inheritdoc />
    public partial class AddAppearanceStat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Appearance",
                table: "PerformanceStats",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Appearance",
                table: "PerformanceStats");
        }
    }
}
