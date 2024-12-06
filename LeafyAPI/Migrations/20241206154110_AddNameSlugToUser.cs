using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeafyAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddNameSlugToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NameSlug",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameSlug",
                table: "AspNetUsers");
        }
    }
}
