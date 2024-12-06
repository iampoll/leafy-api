using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeafyAPI.Migrations
{
    /// <inheritdoc />
    public partial class updateAnnotations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExperienceThreshold",
                table: "Levels",
                type: "int",
                nullable: false,
                defaultValue: 100);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExperienceThreshold",
                table: "Levels");
        }
    }
}
