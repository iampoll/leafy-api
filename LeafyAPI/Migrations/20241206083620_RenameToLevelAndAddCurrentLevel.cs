using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeafyAPI.Migrations
{
    /// <inheritdoc />
    public partial class RenameToLevelAndAddCurrentLevel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Level",
                table: "Levels",
                newName: "CurrentLevel");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CurrentLevel",
                table: "Levels",
                newName: "Level");
        }
    }
}
