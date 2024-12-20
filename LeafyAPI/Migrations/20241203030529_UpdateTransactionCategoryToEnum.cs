﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeafyAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTransactionCategoryToEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Transactions");
        }
    }
}
