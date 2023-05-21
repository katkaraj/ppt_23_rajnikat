using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ppt23.Api.Migrations
{
    /// <inheritdoc />
    public partial class migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_vybaveni",
                table: "vybaveni");

            migrationBuilder.RenameTable(
                name: "vybaveni",
                newName: "Vybavenis");

            migrationBuilder.AddColumn<DateTime>(
                name: "BoughtDateTime",
                table: "Vybavenis",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastRevisionDateTime",
                table: "Vybavenis",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Vybavenis",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vybavenis",
                table: "Vybavenis",
                column: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Vybavenis",
                table: "Vybavenis");

            migrationBuilder.DropColumn(
                name: "BoughtDateTime",
                table: "Vybavenis");

            migrationBuilder.DropColumn(
                name: "LastRevisionDateTime",
                table: "Vybavenis");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Vybavenis");

            migrationBuilder.RenameTable(
                name: "Vybavenis",
                newName: "vybaveni");

            migrationBuilder.AddPrimaryKey(
                name: "PK_vybaveni",
                table: "vybaveni",
                column: "Id");
        }
    }
}
